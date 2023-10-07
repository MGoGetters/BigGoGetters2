using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace Assignment1Attempt4.Areas.Identity.Pages.AssignmentUpload
{
    public class AssignmentUploadModel : PageModel
    {
        [BindProperty]
        public IFormFile TextFile { get; set; }

        public string TextFilePath { get; private set; }

        public async Task OnPostAsync()
        {
            if (TextFile != null && TextFile.Length > 0)
            {
                var userName = HttpContext.User.Identity.Name;
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "submissions");

                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                var fileName = $"{userName}_{DateTime.Now:yyyyMMddHHmmss}.txt"; //will find out a better naming mechanism in the future.
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await TextFile.CopyToAsync(stream);
                }

                TextFilePath = $"/submissions/{fileName}";
            }
        }
    }
}
