using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
namespace PublicCms.Web.Services
{
    public class MongoService
    {
        // According to Mongo docs, these can be placed in a singleton - so that's what we're gonna do.
        private readonly MongoClient _mcl;
        private readonly IMongoDatabase _db;
        private readonly IConfiguration _config;

        public MongoService(IConfiguration config)
        {
            _config = config;

            _mcl = GetMongoClient();
            _db = _mcl.GetDatabase(_config["MongoDB:DBName"]);

        }
        private MongoClient GetMongoClient()
        {
            var connection = _config["MongoDB:ConnectionString"];
            MongoClient client = new MongoClient(connection);
            return client;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _db.GetCollection<T>(name);
        }

    }
}
