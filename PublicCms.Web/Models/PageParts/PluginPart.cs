using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models.PageParts
{
    public class PluginPart : BasePart
    {
        public PluginPart()
        {
            DisplayName = "Insticksmodul";
        }
        public string PluginName { get; set; }
    }
}
