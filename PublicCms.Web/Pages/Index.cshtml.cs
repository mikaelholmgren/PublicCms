using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PublicCms.Data;
using PublicCms.Web.Gateways;
using PublicCms.Web.Models;
using PublicCms.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IContentService _cs;
        private readonly ISettingsService _ss;
        private readonly IVisitorCounterGateway _vcg;
        private readonly SignInManager<AppUser> _sim;

        public IndexModel(IContentService cs, ISettingsService ss, IVisitorCounterGateway vcg, SignInManager<AppUser> sim)
        {
            this._cs = cs;
            _ss = ss;
            this._vcg = vcg;
            this._sim = sim;
        }
        [BindProperty(SupportsGet = true)]
        public string Slug { get; set; }
        public ContentPage CurrentPage { get; set; }
        public SiteSettings CurrentSiteSettings { get; set; }
        public bool ShowSideBar { get; set; }
        public bool HaveComponents { get; set; } = true;
        public async Task<IActionResult> OnGetAsync()
        {
            if (Slug == null)
            {
                var startPage = await _cs.GetStartPageAsync();
                if (startPage == null)
                    CurrentPage = new SimplePage() { Name = "Ingen startsida", TextContent = "Ställ in en startsida för sajten i admin!" };
                else
                    CurrentPage = startPage;
            }
            else
            CurrentPage = await _cs.GetPageBySlugAsync(Slug);
            if (CurrentPage == null) return NotFound();
            CurrentSiteSettings = await _ss.GetSiteSettingsAsync();
            ShowSideBar = CurrentPage.SideBar.Count > 0;
            if (!_sim.IsSignedIn(User)) await _vcg.AddVisitToPageAsync(CurrentPage.Id);
            return Page();
        }
    }
}
