using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublicCms.Web.Services;
using PublicCms.Data;
using Microsoft.AspNetCore.Identity;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection;
using PluginBase;

namespace PublicCms.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AdminService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddSingleton<MongoService>();
            services.AddSingleton<ISiteStatusService, SiteStatusService>();
            services.AddHttpClient<Gateways.VisitorCounterGateway>();
            services.AddSingleton<Gateways.IVisitorCounterGateway, Gateways.VisitorCounterGateway>();
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));
            services.AddIdentity<AppUser, IdentityRole>(options =>
            options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy",
                    policy => policy.RequireRole("Admin"));
            }
                );
            services.AddSession();
            services.AddControllers();
            var mvcBuilder = services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Cms", "AdminPolicy");
            });
            loadPlugins(mvcBuilder);
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

        }
        private void loadPlugins(IMvcBuilder builder)
        {
            var pluginPath = $"{Environment.CurrentDirectory}/plugins";
            if (Directory.Exists(pluginPath))
            {
                var files = Directory.GetFiles(pluginPath);
                foreach (var file in files)
                {
                    if (!file.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    PluginLoadContext loadContext = new PluginLoadContext(file);
                    var asm = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(file)));
                    foreach (var type in asm.GetTypes())
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type))
                        {
                            IPlugin p = Activator.CreateInstance(type) as IPlugin;
                            if (p != null) LoadedPlugins.PluginTypes.Add(p);
                        }
                    }

                    // Let Razor know about the assembly
                    builder.AddApplicationPart(asm);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
