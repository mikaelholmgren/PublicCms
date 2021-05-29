using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models
{
    // This is for lettting MongoDb know the subtypes so it can handle the subclasses
    [BsonKnownTypes(typeof(SimplePage))]
    public class ContentPage : IContentPage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsStartPage { get; set; }
    }
}
