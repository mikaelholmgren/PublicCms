﻿using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using PublicCms.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfiguration _config;
        private readonly MongoService _ms;
        private readonly IMongoCollection<SiteSettings> _siteSettingsCollection;
        public SettingsService(IConfiguration config, MongoService ms)
        {
            _config = config;
            _ms = ms;
            _siteSettingsCollection = _ms.GetCollection<SiteSettings>(_config["MongoDB:SettingsCollectionName"]);
        }
        public async Task CreateSiteSettingsAsync(string siteName)
        {
            var mySettings = await GetSiteSettingsAsync();
            if (mySettings != null) return;
            mySettings = new()
            {
                SiteName = siteName
            };
            await _siteSettingsCollection.InsertOneAsync(mySettings);
        }
        public async Task<SiteSettings> GetSiteSettingsAsync()
        {
            return await _siteSettingsCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
        }
        public async Task SaveSiteSettingsAsync(SiteSettings s)
        {
            await _siteSettingsCollection.ReplaceOneAsync(i => i.Id == s.Id, s);
        }
        public async Task<string> GetSiteName()
        {
            var mySettings = await GetSiteSettingsAsync();
            if (mySettings == null) return string.Empty;
            return mySettings.SiteName;
        }
    }
}