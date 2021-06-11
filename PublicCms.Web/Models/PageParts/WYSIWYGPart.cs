using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models.PageParts
{
    public class WYSIWYGPart : BasePart
    {
        public WYSIWYGPart()
        {
            DisplayName = "Visuell editor";
        }
        public string TextContent { get; set; }
    }
}
