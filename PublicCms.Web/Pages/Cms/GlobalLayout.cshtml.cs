using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models.PageParts;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms
{
    public class GlobalLayoutModel : PageModel
    {
        private readonly ISettingsService _ss;

        public GlobalLayoutModel(ISettingsService ss)
        {
            this._ss = ss;
        }

        public List<BasePart> SideBar { get; set; } = new();
        public List<BasePart> Footer { get; set; } = new();

        public async Task OnGetAsync()
        {
            var siteSettings = await _ss.GetSiteSettingsAsync();
            if (siteSettings.SideBar != null) SideBar = siteSettings.SideBar;
            if (siteSettings.Footer != null) Footer = siteSettings.Footer;

        }
        public async Task<IActionResult> OnGetRemovePartAsync(Models.ZoneTypes zone, int orderNumber)
        {
            List<BasePart> parts = new();
            var CurrentSettings = await _ss.GetSiteSettingsAsync();
            switch (zone)
            {
                case Models.ZoneTypes.SideBar:
                    parts = CurrentSettings.SideBar;
                    break;
                case Models.ZoneTypes.Footer:
                    parts = CurrentSettings.Footer;
                    break;
                default:
                    break;
            }
            var partToRemove = parts.FirstOrDefault(l => l.DisplayOrder == orderNumber);
            parts.Remove(partToRemove);
            await _ss.SaveSiteSettingsAsync(CurrentSettings);
            return RedirectToPage("./GlobalLayout");
        }
        public async Task<IActionResult> OnGetRemoveImageAsync(Models.ZoneTypes zone, int orderNumber)
        {
            List<BasePart> parts = new();
            var CurrentSettings = await _ss.GetSiteSettingsAsync();
            switch (zone)
            {
                case Models.ZoneTypes.SideBar:
                    parts = CurrentSettings.SideBar;
                    break;
                case Models.ZoneTypes.Footer:
                    parts = CurrentSettings.Footer;
                    break;
                default:
                    break;
            }

            var imageToRemove = (Models.PageParts.ImagePart)parts.FirstOrDefault(l => l.DisplayOrder == orderNumber);
            var fileToRemove = $"./wwwroot/{imageToRemove.Src}";
            if (System.IO.File.Exists(fileToRemove))
                System.IO.File.Delete(fileToRemove);
            parts.Remove(imageToRemove);
            await _ss.SaveSiteSettingsAsync(CurrentSettings);
            return RedirectToPage("./GlobalLayout");
        }

    }
}
