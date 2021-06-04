using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models
{
    public class PageNavigationItem
    {
        public Guid PageId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
