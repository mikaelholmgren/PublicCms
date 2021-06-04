using PublicCms.Web.Models.PageParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models
{
    public class SiteSettings : SettingsBase
    {
        public SiteSettings()
        {
            Type = SettingsTypes.Site;
        }
        public string SiteName { get; set; }
        public List<PageNavigationItem> TopNavigation { get; set; }
        public List<BasePart> Footer { get; set; }
    }
}
