using PublicCms.Web.Models.PageParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name ="Sajtens namn")]
        public string SiteName { get; set; }
        public string ThemeCssFile { get; set; } = "";
        public List<PageNavigationItem> TopNavigation { get; set; } = new();
        public List<BasePart> Footer { get; set; }
        [Display(Name ="Text på cookiebanner")]
        public string ConsentText { get; set; }
        public string PrivacyPageUrl { get; set; } = "";
    }
}
