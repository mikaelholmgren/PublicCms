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

        public async Task OnGetAsync()
        {
            if (Slug == null) CurrentPage = await _cs.GetPageBySlugAsync("hem");
            else
            CurrentPage = await _cs.GetPageBySlugAsync(Slug);
        }
    }
}
