using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPlugin.Models
{

    public class APIParameters
    {
        public ParameterItem[] parameter { get; set; }
    }

    public class ParameterItem
    {
        public string name { get; set; }
        public string levelType { get; set; }
        public int level { get; set; }
        public string unit { get; set; }
    }

}
