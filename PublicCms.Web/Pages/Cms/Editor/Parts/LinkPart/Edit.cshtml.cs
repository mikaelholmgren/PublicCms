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
    public class EditModel : PageModel
    {
        private readonly IContentService _cs;

        public EditModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty]
        public LinkInput Input { get; set; } = new();
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
                var lnk = sp.Links.FirstOrDefault(l => l.DisplayOrder == EditIndex);
                Input.Url = lnk.Url;
                Input.Text = lnk.DisplayText;
                Input.NewWindow = lnk.OpenInNewWindow;
                
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                SimplePage sp = (SimplePage)page;
                var lnk = sp.Links.FirstOrDefault(l => l.DisplayOrder == EditIndex);
                lnk.Url = Input.Url;
                lnk.DisplayText = Input.Text;
                lnk.OpenInNewWindow = Input.NewWindow;

                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }

    }
}
