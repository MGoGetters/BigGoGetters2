using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

public class UploadModel : PageModel
{
    [BindProperty]
    public IFormFile Picture { get; set; }

    public string PicturePath { get; private set; }

    public async Task OnPostAsync()
    {
        if (Picture != null && Picture.Length > 0)
        {
            var userName = HttpContext.User.Identity.Name;
            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pfpimages");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var fileName = $"{userName}{Path.GetExtension(Picture.FileName)}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Picture.CopyToAsync(stream);
            }

            PicturePath = $"/pfpimages/{fileName}";
        }
    }
}
