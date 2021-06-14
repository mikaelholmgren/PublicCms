using PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web
{
    public static class LoadedPlugins
    {
        public static List<IPlugin> PluginTypes { get; set; } = new();
    }
}
