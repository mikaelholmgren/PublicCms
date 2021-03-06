using PublicCms.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public interface IContentService
    {
        Task<ContentPage> GetPageBySlugAsync(string slug);
        Task<ContentPage> GetStartPageAsync();
        Task SetStartPageAsync(Guid pageId);
        Task AddPage(ContentPage p);
        Task SavePageAsync(ContentPage p);
        Task<IEnumerable<ContentPage>> GetAllPagesAsync();
        Task<T> GetPageByIdAsync<T>(Guid pageId) where T : ContentPage;
        Task RemovePage(Guid pageId);
    }
}