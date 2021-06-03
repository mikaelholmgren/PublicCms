using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Services;
using PublicCms.Web.Models;
namespace PublicCms.Web.Pages.Cms.Editor.Parts.TextPart
{
    public class EditModel : PageModel
    {
        private readonly IContentService _cs;

        public EditModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty]
        public string TextContent { get; set; }
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
                var txt = (Models.PageParts.TextPart)sp.Parts.FirstOrDefault(l => l.DisplayOrder == EditIndex);
                TextContent = txt.TextContent;
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                int displayOrder = 0;
                SimplePage sp = (SimplePage)page;
                var txt =(Models.PageParts.TextPart) sp.Parts.FirstOrDefault(t => t.DisplayOrder == EditIndex);
                txt.TextContent = TextContent;
                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }

    }
}