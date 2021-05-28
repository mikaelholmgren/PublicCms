using PublicCms.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public interface IContentService
    {
        Task<ContentPage> GetPageByNameAsync(string name);
        Task AddPage(ContentPage p);
        Task SavePageAsync(ContentPage p);
        Task<IEnumerable<ContentPage>> GetAllPagesAsync();

    }
}