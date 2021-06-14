using System;

namespace WeatherPlugin.Models
{
    public class Timesery
    {
        public DateTime validTime { get; set; }
        public Parameter[] parameters { get; set; }
    }
}