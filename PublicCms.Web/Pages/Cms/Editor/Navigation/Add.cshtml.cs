using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Navigation
{
    public class AddModel : PageModel
    {
        private readonly IContentService _cs;
        private readonly ISettingsService _ss;

        public AddModel(IContentService cs, ISettingsService ss)
        {
            this._cs = cs;
            this._ss = ss;
        }
        public List<ContentPage> FilteredPages { get; set; } = new();
        public async Task OnGetAsync()
        {
            var pages = await _cs.GetAllPagesAsync();
            var siteSettings = await _ss.GetSiteSettingsAsync();
            var navIds = siteSettings.TopNavigation.Select(i => i.PageId);
            foreach (var page in pages)
            {
                if (!navIds.Contains(page.Id)) FilteredPages.Add(page);
            }
        }
        public async Task<IActionResult> OnGetAddPageAsync(Guid pageId)
        {
            var siteSettings = await _ss.GetSiteSettingsAsync();
            PageNavigationItem item = new() { PageId = pageId };
            var maxIndex = siteSettings.TopNavigation.Count >0 ? siteSettings.TopNavigation.Max(i => i.DisplayOrder) : 0;
            item.DisplayOrder = maxIndex + 1;
            siteSettings.TopNavigation.Add(item);
            await _ss.SaveSiteSettingsAsync(siteSettings);
            return RedirectToPage("./Index");
        }
    }
}
