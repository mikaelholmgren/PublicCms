using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models.PageParts
{
    public class TextPart : BasePart
    {
        public TextPart()
        {
            DisplayName = "Textblock";
        }
        public string TextContent { get; set; }
    }
}
