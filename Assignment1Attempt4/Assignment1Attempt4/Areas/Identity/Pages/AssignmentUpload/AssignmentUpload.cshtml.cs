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

        // Add a property for the entered text
        [BindProperty]
        public string TextInput { get; set; }

        public string TextFilePath { get; private set; }


        public async Task OnPostAsync()
        {
            var assignmentName = Request.Form["assignmentName"];

            if (!string.IsNullOrEmpty(TextInput))
            {
                // If text is entered, save it as a text file
                var userName = HttpContext.User.Identity.Name;
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "submissions", assignmentName);

                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                var fileName = $"{userName}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                var filePath = Path.Combine(uploadsDirectory, fileName);

                await System.IO.File.WriteAllTextAsync(filePath, TextInput);

                TextFilePath = $"/submissions/{assignmentName}/{fileName}";
            }
            else if (TextFile != null && TextFile.Length > 0)
            {
                // If a file is uploaded, save it as before
                var userName = HttpContext.User.Identity.Name;
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "submissions", assignmentName);

                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                var fileName = $"{userName}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await TextFile.CopyToAsync(stream);
                }

                TextFilePath = $"/submissions/{assignmentName}/{fileName}";
            }
        }
    }
}
