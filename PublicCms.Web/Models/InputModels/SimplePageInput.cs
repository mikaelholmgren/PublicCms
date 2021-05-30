using System.ComponentModel.DataAnnotations;

namespace PublicCms.Web.Models.InputModels
{
    public class SimplePageInput
    {
        [Required]
        [Display(Name = "Sidans rubrik")]
        public string Title { get; set; }
        [Display(Name = "Underrubrik")]
        public string SubHeading { get; set; }
        [Display(Name = "Textinnehåll")]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }
    }
}