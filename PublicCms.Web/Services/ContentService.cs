using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using PublicCms.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public class ContentService : IContentService
    {
        private readonly IConfiguration _config;
        private readonly MongoService _ms;
        private readonly IMongoCollection<ContentPage> _pageCollection;
        public ContentService(IConfiguration config, MongoService ms)
        {
            _config = config;
            _ms = ms;
            _pageCollection = _ms.GetCollection<ContentPage>(_config["MongoDB:PagesCollectionName"]);
        }
        public async Task<ContentPage> GetPageBySlugAsync(string slug)
        {
            return await _pageCollection.Find(n => n.Slug == slug).FirstOrDefaultAsync();
        }
        public async Task<ContentPage> GetStartPageAsync()
        {
            return await _pageCollection.Find(n => n.IsStartPage == true).FirstOrDefaultAsync();
        }

        public async Task AddPage(ContentPage p)
        {
            await _pageCollection.InsertOneAsync(p);
        }
        public async Task SavePageAsync(ContentPage p) 
        {
            
            var filter = Builders<BsonDocument>.Filter.Eq("Id", p.Id);
            await _pageCollection.ReplaceOneAsync(f => p.Id == f.Id, p);
        }
        public async Task<IEnumerable<ContentPage>> GetAllPagesAsync()
        {
            return await _pageCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetPageByIdAsync<T>(Guid pageId) where T : ContentPage
        {
            var collection = _ms.GetCollection<T>(_config["MongoDB:PagesCollectionName"]);
            return await collection.Find(n => n.Id == pageId).FirstOrDefaultAsync();
        }

        public async Task SetStartPageAsync(Guid pageId)
        {
            var currentStartPage = await GetStartPageAsync();
            var newStartPage = await GetPageByIdAsync<ContentPage>(pageId);

            if (currentStartPage == null)
            {
                newStartPage.IsStartPage = true;
                await SavePageAsync(newStartPage);
                return;
            }

            if (pageId == currentStartPage.Id) return;
            currentStartPage.IsStartPage = false;
            await SavePageAsync(currentStartPage);
            newStartPage.IsStartPage = true;
            await SavePageAsync(newStartPage);

        }
    }
}
