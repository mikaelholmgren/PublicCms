using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Models.InputModels;
using PublicCms.Web.Services;
using PublicCms.Web.Extensions;
using PublicCms.Web.Models.PageParts;

namespace PublicCms.Web.Pages.Cms
{
    public class EditPageModel : PageModel
    {
        private readonly IContentService _cs;

        public EditPageModel(IContentService cs)
        {
            this._cs = cs;
        }
        public SimplePage CurrentPage { get; set; }
        [BindProperty]
        public SimplePageInput MainInput { get; set; }
        [BindProperty]
        public LinkInput NewLinkInput { get; set; }
        public async Task OnGetAsync(bool isEditing = false)
        {
            if (isEditing)
            {
                CurrentPage = HttpContext.Session.GetPageFromSession<SimplePage>();
                FillInputModels();
            }
        }

        private void FillInputModels()
        {
            MainInput = new()
            {
                Title = CurrentPage.Name,
                SubHeading = CurrentPage.Heading,
                TextContent = CurrentPage.TextContent
            };
        }

        public async Task<IActionResult> OnPostMainFrmAsync()
        {
            if (MainInput.Title == null) return Page();
            SimplePage page = new()
            {
                Name = MainInput.Title,
                Heading = MainInput.SubHeading,
                TextContent = MainInput.TextContent
            };
            HttpContext.Session.SavePageToSession(page);
            return RedirectToPage("./EditPage", new { isEditing = true });
        }
        public async Task<IActionResult> OnPostNewLinkFrmAsync()
        {
            if (NewLinkInput.Url == null) return Page();
            LinkPart link = new()
            {
                Url = NewLinkInput.Url,
                DisplayText = NewLinkInput.Text,
                OpenInNewWindow = NewLinkInput.NewWindow
            };
            var page = HttpContext.Session.GetPageFromSession<SimplePage>();
            if (page.Links == null) page.Links = new();
            page.Links.Add(link);
            HttpContext.Session.SavePageToSession(page);
            return RedirectToPage("./EditPage", new { isEditing = true });
        }

    }
}
