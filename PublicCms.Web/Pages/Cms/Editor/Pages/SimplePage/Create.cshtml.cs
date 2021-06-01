using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models.InputModels;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Pages.SimplePage
{
    public class CreateModel : PageModel
    {
        private readonly IContentService _cs;

        public CreateModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty]
        public SimplePageInput Input { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            Models.SimplePage page = new()
            {
                Name = Input.Title,
                Slug = Input.Title.ToLower().Replace(" ", "-")
            };

            await _cs.AddPage(page);
            return RedirectToPage("./Edit", new { pageId = page.Id });
        }
    }
}
