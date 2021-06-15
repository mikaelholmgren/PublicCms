using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Models.InputModels;
using PublicCms.Web.Models.PageParts;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Parts.LinkPart
{
    public class CreateModel : PageModel
    {
        private readonly IContentService _cs;
        private readonly ISettingsService _ss;

        public CreateModel(IContentService cs, ISettingsService ss)
        {
            this._cs = cs;
            this._ss = ss;
        }
        [BindProperty]
        public LinkInput Input { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid? PageId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        [BindProperty(SupportsGet = true)]
        public ZoneTypes Zone { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            List<BasePart> parts = new();
            if (PageId != null)
            {
                var page = await _cs.GetPageByIdAsync<ContentPage>(PageId.Value);
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
                    if (parts.Count == 0) displayOrder++;
                    else
                        displayOrder = parts.Max(m => m.DisplayOrder) + 1;
                    Models.PageParts.LinkPart lp = new()
                    {
                        Url = Input.Url,
                        DisplayText = Input.Text,
                        OpenInNewWindow = Input.NewWindow,
                        DisplayOrder = displayOrder

                    };
                    parts.Add(lp);
                    await _cs.SavePageAsync(sp);
                }
            }
            else // global layout
            {
                int displayOrder = 0;
                SiteSettings sp = await _ss.GetSiteSettingsAsync();
                switch (Zone)
                {
                    case ZoneTypes.SideBar:
                        parts = sp.SideBar;
                        break;
                    case ZoneTypes.Footer:
                        parts = sp.Footer;
                        break;
                    default:
                        break;
                }
                if (parts.Count == 0) displayOrder++;
                else
                    displayOrder = parts.Max(m => m.DisplayOrder) + 1;
                Models.PageParts.LinkPart lp = new()
                {
                    Url = Input.Url,
                    DisplayText = Input.Text,
                    OpenInNewWindow = Input.NewWindow,
                    DisplayOrder = displayOrder

                };
                parts.Add(lp);
                await _ss.SaveSiteSettingsAsync(sp);

            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }
    }
}
