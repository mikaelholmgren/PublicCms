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
        public async Task OnGetAsync(Guid pageId)
        {
            CurrentPage = await _cs.GetPageByIdAsync<Models.SimplePage>(pageId);
        }
    }
}
