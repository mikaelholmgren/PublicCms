using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Gateways;
using PublicCms.Web.Models;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.VisitorStats
{
    public class IndexModel : PageModel
    {
        private readonly IContentService _cs;
        private readonly IVisitorCounterGateway _vcg;

        public IndexModel(IContentService cs, IVisitorCounterGateway vcg)
        {
            this._cs = cs;
            this._vcg = vcg;
        }


        public IEnumerable<VisitorModel> CurrentStats { get; set; }
        public IEnumerable<ContentPage> AllPages { get; private set; }
        public bool ServiceOk { get; set; }
        public async Task OnGetAsync()
        {
           ServiceOk = await _vcg.VisitorCounterServiceAvailable();
            CurrentStats = await _vcg.GetAllVisitorStatsAsync();
            AllPages = await _cs.GetAllPagesAsync();
        }
    }
}
