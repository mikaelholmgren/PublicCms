using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicCms.Web.Models.InputModels
{
    public class LinkInput
    {
        [Required]
        [Display(Name = "Länkadress (URL)")]
        public string Url { get; set; }
        [Display(Name = "Länktext")]
        public string Text { get; set; }
        [Display(Name = "Öppna i nytt fönster")]
        public bool NewWindow { get; set; }
    }
}
