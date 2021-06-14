using MongoDB.Bson.Serialization.Attributes;

namespace PublicCms.Web.Models.PageParts
{
    [BsonKnownTypes(typeof(ImagePart), typeof(LinkPart), typeof(TextPart), typeof(WYSIWYGPart), typeof(PluginPart))]
    public class BasePart
    {
        public int DisplayOrder { get; set; }
        [BsonIgnore]
        public string DisplayName { get; set; }
    }
}