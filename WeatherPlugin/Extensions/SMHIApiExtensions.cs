using WeatherPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPlugin.Extensions
{
    public static class SMHIApiExtensions
    {
        public static float GetTemperature(this Parameter[] p)
        {
            
            Parameter param = GetParameterByName(p, "t");
            return param.values[0];
        }

        public static WeatherSymbol GetWeatherSymbol(this Parameter[] p)
        {

            Parameter param = GetParameterByName(p, "Wsymb2");
            return (WeatherSymbol)param.values[0];
        }

        private static Parameter GetParameterByName(Parameter[] par, string paramName)
        {
            return par.Where(n => n.name == paramName).FirstOrDefault();
        }
    }
}
