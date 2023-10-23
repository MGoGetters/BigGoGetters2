using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Assignment1Attempt4.Areas.Identity.Pages.AssignmentUpload
{
    public class AssignmentUploadModel : PageModel
    {
        [BindProperty]
        public IFormFile TextFile { get; set; }

        [BindProperty]
        public string TextInput { get; set; }

        public string TextFilePath { get; private set; }
        public async Task OnPostAsync()
        {
            var assignmentName = Request.Form["assignmentName"];
            var userName = HttpContext.User.Identity.Name;
            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "submissions", assignmentName);

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var firstName = HttpContext.Session.GetString("CurrentFName");
            var lastName = HttpContext.Session.GetString("CurrentLName");

            string fileName;

            if (TextFile != null && TextFile.Length > 0)
            {
                string originalExtension = Path.GetExtension(TextFile.FileName);
                if (originalExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    fileName = $"{firstName}_{lastName}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                }
                else
                {
                    fileName = $"{firstName}_{lastName}_{DateTime.Now:yyyyMMddHHmmss}{originalExtension}";
                }

                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await TextFile.CopyToAsync(stream);
                }

                TextFilePath = $"/submissions/{assignmentName}/{fileName}";
            }
            else if (!string.IsNullOrEmpty(TextInput))
            {
                fileName = $"{firstName}_{lastName}_{DateTime.Now:yyyyMMddHHmmss}.txt";
                var filePath = Path.Combine(uploadsDirectory, fileName);
                await System.IO.File.WriteAllTextAsync(filePath, TextInput);
                TextFilePath = $"/submissions/{assignmentName}/{fileName}";
            }
        }
    }
}
