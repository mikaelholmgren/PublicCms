using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models.InputModels
{
    public class ImageInput
    {
        [Display(Name ="Bild")]
        public string Src { get; set; }
        [Display(Name ="Alternativ text")]
        public string AltText { get; set; }
        [Display(Name ="Bredd på bild (antal pixlar)")]
        public int? Width { get; set; }

    }
}
