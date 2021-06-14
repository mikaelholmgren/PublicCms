using WeatherPlugin.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;

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
        public static string GetDisplayText(this Enum val)
        {
            if (val.GetType().GetField(val.ToString()) == null) return string.Empty;
            return val.GetType().GetField(val.ToString()).GetCustomAttribute<DisplayAttribute>().GetName();
        }
        private static Parameter GetParameterByName(Parameter[] par, string paramName)
        {
            return par.Where(n => n.name == paramName).FirstOrDefault();
        }
    }
}
