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
            CurrentNavItems = mySettings.TopNavigation.OrderBy(d => d.DisplayOrder).ToList();
        }
        public async Task<IActionResult> OnGetRemove(int index)
        {
            var mySettings = await _ss.GetSiteSettingsAsync();
            mySettings.TopNavigation.RemoveAll(r => r.DisplayOrder == index);
            await _ss.SaveSiteSettingsAsync(mySettings);
            return RedirectToPage("./index");
        }
        public async Task<IActionResult> OnGetReorderAsync(int direction, int item)
        {
            var mySettings = await _ss.GetSiteSettingsAsync();
            if (direction == 0) // up
            {
                int prevIndex = mySettings.TopNavigation.Where(d => d.DisplayOrder < item).Max(m => m.DisplayOrder);
                var curItem = mySettings.TopNavigation.FirstOrDefault(i => i.DisplayOrder == item);
                var prevItem = mySettings.TopNavigation.FirstOrDefault(d => d.DisplayOrder == prevIndex);
                curItem.DisplayOrder = prevIndex;
                prevItem.DisplayOrder = item;
            }
            if (direction == 1) // down
            {
                int nextIndex = mySettings.TopNavigation.Where(d => d.DisplayOrder > item).Min(m => m.DisplayOrder);
                var curItem = mySettings.TopNavigation.FirstOrDefault(i => i.DisplayOrder == item);
                var nextItem = mySettings.TopNavigation.FirstOrDefault(d => d.DisplayOrder == nextIndex);
                curItem.DisplayOrder = nextIndex;
                nextItem.DisplayOrder = item;
            }

            await _ss.SaveSiteSettingsAsync(mySettings);
            return RedirectToPage("./index");
        }

    }
}
