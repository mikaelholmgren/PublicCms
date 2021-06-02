using MongoDB.Bson.Serialization.Attributes;

namespace PublicCms.Web.Models.PageParts
{
    [BsonKnownTypes(typeof(ImagePart), typeof(LinkPart), typeof(TextPart))]
    public class BasePart
    {
        public int DisplayOrder { get; set; }
    }
}