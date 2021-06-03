namespace PublicCms.Web.Models.PageParts
{
    public class ImagePart : BasePart
    {
        public ImagePart()
        {
            DisplayName = "Bild";
        }
        public string Src { get; set; }
        public string AltText { get; set; }
        public int? Width { get; set; }
    }
}