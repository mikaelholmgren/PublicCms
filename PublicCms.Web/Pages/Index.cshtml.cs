using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PublicCms.Web.Models;
using PublicCms.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IContentService _cs;

        public IndexModel(IContentService cs)
        {
            this._cs = cs;
        }
        [BindProperty(SupportsGet = true)]
        public string Slug { get; set; }
        public ContentPage CurrentPage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Slug == null)
            {
                var startPage = await _cs.GetStartPageAsync();
                if (startPage == null)
                    CurrentPage = new SimplePage() { Name = "Ingen startsida", TextContent = "Ställ in en startsida för sajten i admin!" };
                else
                    CurrentPage = startPage;
            }
            else
            CurrentPage = await _cs.GetPageBySlugAsync(Slug);
            if (CurrentPage == null) return NotFound();
            return Page();
        }
    }
}
