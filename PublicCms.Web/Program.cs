using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PublicCms.Data;
using PublicCms.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            // Create the db and admin user if it doesn't exist
            var scopeFactory = builder.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var adminService = scope.ServiceProvider.GetRequiredService<AdminService>();
                var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                if (db.Database.EnsureCreated())
                {
                    // normally you wouldn't hardcode this password. But for demo purposes, its set here.
                    await adminService.CreateAdmin("Admin123!");
                }
                var settings = scope.ServiceProvider.GetRequiredService<ISettingsService>();
                await settings.CreateSiteSettingsAsync("Vår sajt");
                if (!System.IO.Directory.Exists("./wwwroot/Uploads/Images")) Directory.CreateDirectory("./wwwroot/Uploads/Images");
            }
            builder.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
