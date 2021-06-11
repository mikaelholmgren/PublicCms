using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models.PageParts;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Pages.SimplePage
{
    public class EditModel : PageModel
    {
        private readonly IContentService _cs;

        public EditModel(IContentService cs)
        {
            this._cs = cs;
        }
        public Models.SimplePage CurrentPage { get; set; }
        [BindProperty]
        public Models.InputModels.SimplePageInput MainInput { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid PageId { get; set; }
        public async Task OnGetAsync()
        {
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(PageId);
            if (!string.IsNullOrEmpty(CurrentPage.TextContent)) MainInput.TextContent = CurrentPage.TextContent;
        }
        public async Task<IActionResult> OnPostMainFrmAsync()
        {
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(PageId);
            CurrentPage.Name = MainInput.Title;
            CurrentPage.Slug = MainInput.Title.ToLower().Replace(" ", "-");
            await _cs.SavePageAsync(CurrentPage);
            return RedirectToPage("./edit", new { pageId = PageId });
        }
        public async Task<IActionResult> OnGetRemovePartAsync(Models.ZoneTypes zone, int orderNumber)
        {
            List<BasePart> parts = new();
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(PageId);
            switch (zone)
            {
                case Models.ZoneTypes.Main:
                    parts = CurrentPage.Parts;
                    break;
                case Models.ZoneTypes.SideBar:
                    parts = CurrentPage.SideBar;
                    break;
                case Models.ZoneTypes.Footer:
                    parts = CurrentPage.Footer;
                    break;
                default:
                    break;
            }
            var partToRemove = parts.FirstOrDefault(l => l.DisplayOrder == orderNumber);
            parts.Remove(partToRemove);
            await _cs.SavePageAsync(CurrentPage);
            return RedirectToPage("./edit", new { pageId = PageId });
        }
        public async Task<IActionResult> OnGetRemoveImageAsync(Models.ZoneTypes zone, int orderNumber)
        {
            List<BasePart> parts = new();
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(PageId);
            switch (zone)
            {
                case Models.ZoneTypes.Main:
                    parts = CurrentPage.Parts;
                    break;
                case Models.ZoneTypes.SideBar:
                    parts = CurrentPage.SideBar;
                    break;
                case Models.ZoneTypes.Footer:
                    parts = CurrentPage.Footer;
                    break;
                default:
                    break;
            }

            var imageToRemove = (Models.PageParts.ImagePart)parts.FirstOrDefault(l => l.DisplayOrder == orderNumber);
            var fileToRemove = $"./wwwroot/{imageToRemove.Src}";
            if (System.IO.File.Exists(fileToRemove))
                System.IO.File.Delete(fileToRemove);
            parts.Remove(imageToRemove);
            await _cs.SavePageAsync(CurrentPage);
            return RedirectToPage("./edit", new { pageId = PageId });
        }

    }
}
