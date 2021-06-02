using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Models.InputModels;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Parts.LinkPart
{
    public class CreateModel : PageModel
    {
        private readonly IContentService _cs;

        public CreateModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty]
        public LinkInput Input { get; set; } = new();
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
                Models.PageParts.LinkPart lp = new()
                {
                    Url = Input.Url,
                    DisplayText = Input.Text,
                    DisplayOrder = displayOrder

                };
                sp.Parts.Add(lp);
                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }
    }
}
