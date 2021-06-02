using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Parts.TextPart
{
    public class CreateModel : PageModel
    {
        private readonly IContentService _cs;

        public CreateModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty]
        public string TextContent { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid PageId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                int displayOrder = 0;
                SimplePage sp = (SimplePage)page;
                if (sp.Parts.Count == 0) displayOrder++;
                else
                    displayOrder = sp.Parts.Max(m => m.DisplayOrder) + 1;
                Models.PageParts.TextPart tp = new()
                {
                    DisplayOrder = displayOrder,
                    TextContent = TextContent
                };
                sp.Parts.Add(tp);
                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }

    }
}
