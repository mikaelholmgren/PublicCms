using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms
{
    public class SettingsModel : PageModel
    {
        private readonly ISettingsService _ss;

        public SettingsModel(ISettingsService ss)
        {
            this._ss = ss;
        }
        [BindProperty]
        public string SiteName { get; set; }
        [BindProperty]
        public string ConsentText { get; set; }
        [BindProperty]
        public string PrivacyPageUrl { get; set; }
        public async Task OnGetAsync()
        {
            var settings = await _ss.GetSiteSettingsAsync();
            SiteName = settings.SiteName;
            ConsentText = settings.ConsentText;
            PrivacyPageUrl = settings.PrivacyPageUrl;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var settings = await _ss.GetSiteSettingsAsync();
            settings.SiteName = SiteName;
            settings.ConsentText = ConsentText;
            settings.PrivacyPageUrl = PrivacyPageUrl;
            await _ss.SaveSiteSettingsAsync(settings);
            return RedirectToPage("./settings");
        }
    }
}
