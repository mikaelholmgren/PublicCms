using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PublicCms.Web.Models;
using PublicCms.Web.Models.PageParts;
using PublicCms.Web.Services;

namespace PublicCms.Web.Pages.Cms.Editor.Parts.ImagePart
{
    public class CreateModel : PageModel
    {
        private readonly IContentService _cs;

        public CreateModel(IContentService cs)
        {
            this._cs = cs;
        }
//        [Required]
        [BindProperty]
        public IFormFile UploadedFile { get; set; }
        [BindProperty]
        public Models.InputModels.ImageInput Input { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public Guid PageId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }
        [BindProperty(SupportsGet = true)]
        public ZoneTypes Zone { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            List<BasePart> parts = new();

            if (!ModelState.IsValid) return Page();
            if (!System.IO.Directory.Exists("./wwwroot/Uploads/Images")) Directory.CreateDirectory("./wwwroot/Uploads/Images");
            var file = "./wwwroot/Uploads/Images/" + UploadedFile.FileName;
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(fileStream);
            }
            var filePath = "Uploads/Images/" + UploadedFile.FileName;

            var page = await _cs.GetPageByIdAsync<ContentPage>(PageId);
            if (page is SimplePage)
            {
                SimplePage sp = (SimplePage)page;
                switch (Zone)
                {
                    case ZoneTypes.Main:
                        parts = sp.Parts;
                        break;
                    case ZoneTypes.SideBar:
                        parts = sp.SideBar;
                        break;
                    case ZoneTypes.Footer:
                        parts = sp.Footer;
                        break;
                    default:
                        break;
                }
                Models.PageParts.ImagePart part = new()
                {
                    Src = filePath,
                    AltText = Input.AltText,
                    Width = Input.Width
                };
                var maxIndex = parts.Max(m => m.DisplayOrder);
                part.DisplayOrder = maxIndex + 1;
                parts.Add(part);
                await _cs.SavePageAsync(sp);
            }
            return RedirectToPage(ReturnUrl, new { pageId = PageId });
        }
    }
}
