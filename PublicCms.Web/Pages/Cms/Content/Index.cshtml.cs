using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Models.PageParts;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Content
{
    public class IndexModel : PageModel
    {
        private readonly IContentService _cs;
        private readonly ISettingsService _ss;

        public IndexModel(IContentService cs, ISettingsService ss)
        {
            this._cs = cs;
            this._ss = ss;
        }
        public IEnumerable<ContentPage> Pages { get; set; } = new List<ContentPage>();
        public async Task OnGetAsync()
        {
            Pages = await _cs.GetAllPagesAsync();
        }
        public async Task<IActionResult> OnGetSetAsStartPageAsync(Guid pageId)
        {
            await _cs.SetStartPageAsync(pageId);
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnGetRemovePageAsync(Guid pageId)
        {
            var sp = await _cs.GetPageByIdAsync<SimplePage>(pageId);
            if (sp != null)
            {
                var settings = await _ss.GetSiteSettingsAsync();
                var inNav = settings.TopNavigation.Where(i => i.PageId == pageId).FirstOrDefault();
                if (inNav != null)
                {
                    settings.TopNavigation.Remove(inNav);
                    _ss.SaveSiteSettingsAsync(settings);
                }
                var mainParts = sp.Parts;
                var sideParts = sp.SideBar;
                var footParts = sp.Footer;
                foreach (var imgPart in mainParts.Where(p => p is ImagePart))
                {
                    ImagePart img = (ImagePart)imgPart;
                    var fileToRemove = $"./wwwroot/{img.Src}";
                    if (System.IO.File.Exists(fileToRemove))
                        System.IO.File.Delete(fileToRemove);

                }
                foreach (var imgPart in sideParts.Where(p => p is ImagePart))
                {
                    ImagePart img = (ImagePart)imgPart;
                    var fileToRemove = $"./wwwroot/{img.Src}";
                    if (System.IO.File.Exists(fileToRemove))
                        System.IO.File.Delete(fileToRemove);

                }
                foreach (var imgPart in footParts.Where(p => p is ImagePart))
                {
                    ImagePart img = (ImagePart)imgPart;
                    var fileToRemove = $"./wwwroot/{img.Src}";
                    if (System.IO.File.Exists(fileToRemove))
                        System.IO.File.Delete(fileToRemove);

                }
                await _cs.RemovePage(pageId);
            }
            return RedirectToPage("./Index");
        }
    }
}
