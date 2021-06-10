using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Services;
using PublicCms.Web.Models;
using PublicCms.Web.Models.PageParts;

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
        [BindProperty(SupportsGet = true)]
        public ZoneTypes Zone { get; set; }

        public async Task OnGetAsync()
        {
            List<BasePart> parts = new();

            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                SimplePage sp = (SimplePage)page;
                switch (Zone)
                {
                    case ZoneTypes.Main:
                        parts = sp.Parts;
                        break;
                    case ZoneTypes.SideBar:
                        parts = sp.SideBar;
                        break;
                    case ZoneTypes.Footer:
                        parts = sp.Footer;
                        break;
                    default:
                        break;
                }
                var txt = (Models.PageParts.TextPart)parts.FirstOrDefault(l => l.DisplayOrder == EditIndex);
                TextContent = txt.TextContent;
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            List<BasePart> parts = new();

            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                int displayOrder = 0;
                SimplePage sp = (SimplePage)page;
                switch (Zone)
                {
                    case ZoneTypes.Main:
                        parts = sp.Parts;
                        break;
                    case ZoneTypes.SideBar:
                        parts = sp.SideBar;
                        break;
                    case ZoneTypes.Footer:
                        parts = sp.Footer;
                        break;
                    default:
                        break;
                }
                var txt =(Models.PageParts.TextPart) parts.FirstOrDefault(t => t.DisplayOrder == EditIndex);
                txt.TextContent = TextContent;
                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }

    }
}
