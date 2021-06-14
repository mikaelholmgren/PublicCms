using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluginBase
{
    public interface IPlugin
    {
        public string DisplayName { get; }
        public Type ComponentType { get; }
    }
}
