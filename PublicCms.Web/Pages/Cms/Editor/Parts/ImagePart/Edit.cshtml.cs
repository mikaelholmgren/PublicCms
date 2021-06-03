using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Parts.ImagePart
{
    public class EditModel : PageModel
    {
        private readonly IContentService _cs;

        public EditModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty]
        public Models.InputModels.ImageInput Input { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid PageId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        [BindProperty(SupportsGet = true)]
        public int EditIndex { get; set; }
        public async Task OnGetAsync()
        {
            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                SimplePage sp = (SimplePage)page;
                var part = (Models.PageParts.ImagePart)sp.Parts.FirstOrDefault(i => i.DisplayOrder == EditIndex);
                Input.Src = part.Src;
                Input.AltText = part.AltText;
                Input.Width = part.Width;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                SimplePage sp = (SimplePage)page;
                var part = (Models.PageParts.ImagePart)sp.Parts.FirstOrDefault(i => i.DisplayOrder == EditIndex);
                part.AltText = Input.AltText;
                part.Width = Input.Width;
                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }

    }
}
