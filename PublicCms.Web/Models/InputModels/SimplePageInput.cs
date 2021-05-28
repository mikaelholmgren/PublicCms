using System.ComponentModel.DataAnnotations;

namespace PublicCms.Web.Models.InputModels
{
    public class SimplePageInput
    {
        [Required]
        [Display(Name = "Sidans namn")]
        public string Name { get; set; }
        [Display(Name = "Rubrik")]
        public string Heading { get; set; }
        [Display(Name = "Textinnehåll")]
        [DataType(DataType.MultilineText)]
        public string TextContent { get; set; }
    }
}