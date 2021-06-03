using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models.PageParts
{
    public class LinkPart : BasePart
    {
        public LinkPart()
        {
            DisplayName = "Länk";
        }
        public string Url { get; set; }
        public string DisplayText { get; set; }
        public ImagePart Image { get; set; }
        public bool OpenInNewWindow { get; set; }
    }
}
