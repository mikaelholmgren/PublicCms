using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public async Task OnGetAsync(Guid pageId)
        {
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(pageId);
            if (!string.IsNullOrEmpty(CurrentPage.TextContent)) MainInput.TextContent = CurrentPage.TextContent;
        }
        public async Task<IActionResult> OnPostMainFrmAsync(Guid pageId)
        {
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(pageId);
            CurrentPage.Name = MainInput.Title;
            CurrentPage.Slug = MainInput.Title.ToLower().Replace(" ", "-");
            CurrentPage.Heading = MainInput.SubHeading;
            CurrentPage.TextContent = MainInput.TextContent;
            await _cs.SavePageAsync(CurrentPage);
            return RedirectToPage("./edit", new { pageId = pageId });
        }
    }
}
