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
        public async Task<ContentPage> GetPageByNameAsync(string name)
        {
            return await _pageCollection.Find(n => n.Name == name).FirstOrDefaultAsync();
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

    }
}
