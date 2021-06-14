using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPlugin.Models
{

    public class SMHIModel
    {
        public DateTime approvedTime { get; set; }
        public DateTime referenceTime { get; set; }
        public Geometry geometry { get; set; }
        public Timesery[] timeSeries { get; set; }
    }
}