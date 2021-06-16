using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicCms.Web.Models;
using PublicCms.Web.Services;
using PublicCms.Web.SiteMapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.SiteMapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteMapController : ControllerBase
    {
        private readonly IContentService _cs;

        public SiteMapController(IContentService cs)
        {
            this._cs = cs;
        }
        [HttpGet]
        public async Task<IEnumerable<SiteMapModel>> GetAllPagesAsync()
        {
            var allPages = await _cs.GetAllPagesAsync();
            List<SiteMapModel> list = new();
            foreach (var pg in allPages)
            {
                list.Add(new SiteMapModel()
                {
                    PageName = pg.Name,
                    PageUrl = $"/{pg.Slug}"
                });
            }
            return list;
        }
    }
}
