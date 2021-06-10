using MongoDB.Bson.Serialization.Attributes;
using PublicCms.Web.Models.PageParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models
{
    public class SimplePage : ContentPage
    {
        public SimplePage()
        {
            DisplayComponent = typeof(Components.SimplePage);
        }
        public List<BasePart> Parts { get; set; } = new();
        public string Heading { get; set; }
        public string TextContent { get; set; }
        public List<LinkPart> Links { get; set; } = new();
        public ImagePart Image { get; set; }

    }
}
