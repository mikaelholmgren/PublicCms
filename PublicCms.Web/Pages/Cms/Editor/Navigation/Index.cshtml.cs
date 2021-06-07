using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Navigation
{
    public class IndexModel : PageModel
    {
        private readonly ISettingsService _ss;

        public IndexModel(ISettingsService ss)
        {
            this._ss = ss;
        }

        public List<Models.PageNavigationItem> CurrentNavItems { get; set; } = new();

        public async Task OnGetAsync()
        {
            var mySettings = await _ss.GetSiteSettingsAsync();
            CurrentNavItems = mySettings.TopNavigation;
        }
        public async Task<IActionResult> OnGetRemove(int index)
        {
            var mySettings = await _ss.GetSiteSettingsAsync();
            mySettings.TopNavigation.RemoveAll(r => r.DisplayOrder == index);
            await _ss.SaveSiteSettingsAsync(mySettings);
            return RedirectToPage("./index");
        }
    }
}
