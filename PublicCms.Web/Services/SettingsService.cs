using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using PublicCms.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IConfiguration _config;
        private readonly MongoService _ms;
        private readonly ISiteStatusService _sss;
        private readonly IMongoCollection<SiteSettings> _siteSettingsCollection;
        private string _themesPath = "./wwwroot/css/themes";

        public SettingsService(IConfiguration config, MongoService ms, ISiteStatusService sss)
        {
            _config = config;
            _ms = ms;
            this._sss = sss;
            _siteSettingsCollection = _ms.GetCollection<SiteSettings>(_config["MongoDB:SettingsCollectionName"]);
        }
        public async Task CreateSiteSettingsAsync(string siteName)
        {
            SiteSettings mySettings = null;
            
            mySettings = await GetSiteSettingsAsync();

            if (mySettings != null) return;
            mySettings = new()
            {
                SiteName = siteName,
                TopNavigation = new(),
                SideBar = new(),
                Footer = new()
            };
            await _siteSettingsCollection.InsertOneAsync(mySettings);
        }
        public async Task<SiteSettings> GetSiteSettingsAsync()
        {
            SiteSettings mySettings = await _siteSettingsCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            if (mySettings == null) return null;
            // Make sure none of the list parts have a null value
            if (mySettings.TopNavigation == null) mySettings.TopNavigation = new();
            if (mySettings.SideBar == null) mySettings.SideBar = new();
            if (mySettings.Footer == null) mySettings.Footer = new();
            return mySettings;
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
        public List<(string FileName, string DisplayText)> GetThemeFiles()
        {
            List<(string FileName, string DisplayText)> entries = new();
            var files = Directory.GetFileSystemEntries(_themesPath);
            foreach (var file in files)
            {
                var lines = File.ReadLines(file);
                var text = lines.First();
                text = text.Substring(2, text.Length - 4);
                entries.Add((Path.GetFileName(file), text));
            }
            return entries;
        }
    }
}
