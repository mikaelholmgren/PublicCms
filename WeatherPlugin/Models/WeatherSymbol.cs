using System.ComponentModel.DataAnnotations;

namespace WeatherPlugin.Models
{
    public enum WeatherSymbol
    {
        [Display(Name = "Klart")]
        ClearSky = 1,
        [Display(Name = "Nästan klart")]
        NearlyClearSky,
        [Display(Name = "Växlande molnighet")]
        VariableCloudiness,
        [Display(Name = "Halvklart")]
        HalfclearSky,
        [Display(Name = "Molnigt")]
        CloudySky,
        [Display(Name = "Mulet")]
        Overcast,
        [Display(Name = "Dimmigt")]
        Fog,
        [Display(Name = "lätta regnskurar")]
        LightRainShowers,
        [Display(Name = "medel regnskurar")]
        ModerateRainShowers,
        [Display(Name = "kraftigt regn")]
        HeavyRainShowers,
        [Display(Name = "Åska")]
        Thunderstorm,
        [Display(Name = "lätt snöblandat regn")]
        LightSleetShowers,
        [Display(Name = "medel snöblandat regn")]
        ModerateSleetShowers,
        [Display(Name = "kraftigt snöblandat regn")]
        HeavySleetShowers,
        [Display(Name = "lätta snöbyar")]
        LightSnowShowers,
        [Display(Name = "medel snöbyar")]
        ModerateSnowShowers,
        [Display(Name = "kraftiga snöbyar")]
        HeavySnowShowers,
        [Display(Name = "lätt regn")]
        LightRain,
        [Display(Name = "regn")]
        ModerateRain,
        [Display(Name = "kraftigt regn")]
        HeavyRain,
        [Display(Name = "Åska")]
        Thunder,
        [Display(Name = "lätt snöblandat regn")]
        LightSleet,
        [Display(Name = "snöblandat regn")]
        ModerateSleet,
        [Display(Name = "kraftigt snöblandat regn")]
        HeavySleet,
        [Display(Name = "lätt snöfall")]
        LightSnowfall,
        [Display(Name = "snöfall")]
        ModerateSnowfall,
        [Display(Name = "kraftigt snöfall")]
        HeavySnowfall
    }
}