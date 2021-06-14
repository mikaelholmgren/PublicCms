using PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPlugin
{
    public class Plugin : IPlugin
    {
        public string DisplayName { get => "Väder"; }
        public Type ComponentType { get =>  typeof(SMHI); }
    }
}
