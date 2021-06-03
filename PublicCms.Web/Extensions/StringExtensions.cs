using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Extensions
{
    public static class StringExtensions
    {
        // This makes a string displayable in HTML, fix line breaks etc.
        public static string Displayify(this string s)
        {
            return s.Replace("\n", "<br />");
        }
    }
}
