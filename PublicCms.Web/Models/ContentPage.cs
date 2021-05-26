using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models
{
    public class ContentPage : IContentPage
    {
        public Guid Id { get; internal set; }

        public string Name { get; internal set; }
        public string Slug { get; set; }
    }
}
