using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Content
{
    public class IndexModel : PageModel
    {
        private readonly IContentService _cs;
        
        public IndexModel(IContentService cs)
        {
            this._cs = cs;
        }
        public IEnumerable<ContentPage> Pages { get; set; } = new List<ContentPage>();
        public async Task OnGetAsync()
        {
            Pages = await _cs.GetAllPagesAsync();
        }
        public async Task<IActionResult> OnGetSetAsStartPageAsync(Guid pageId)
        {
            await _cs.SetStartPageAsync(pageId);
            return RedirectToPage("./Index");
        }
    }
}
