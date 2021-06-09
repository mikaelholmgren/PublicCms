using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Themes
{
    public class IndexModel : PageModel
    {
        private readonly ISettingsService _ss;

        public IndexModel(ISettingsService ss)
        {
            this._ss = ss;
        }

        public SiteSettings SiteSettings { get; set; }
        public List<(string FileName, string DisplayText)> Themes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedTheme { get; set; }
        public async Task OnGetAsync(string select)
        {
            SiteSettings = await _ss.GetSiteSettingsAsync();
            Themes = _ss.GetThemeFiles();
            if (!string.IsNullOrEmpty(select))
            {
                SiteSettings.ThemeCssFile = select;
                await _ss.SaveSiteSettingsAsync(SiteSettings);
            }
        }
    }
}
