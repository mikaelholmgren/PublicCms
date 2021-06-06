using PublicCms.Web.Models;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public interface ISettingsService
    {
        Task CreateSiteSettingsAsync(string siteName);
        Task<SiteSettings> GetSiteSettingsAsync();
        Task SaveSiteSettingsAsync(SiteSettings s);
        Task<string> GetSiteName();

    }
}