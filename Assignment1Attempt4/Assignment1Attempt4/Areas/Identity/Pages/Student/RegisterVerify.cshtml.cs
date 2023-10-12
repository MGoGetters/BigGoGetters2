using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1Attempt4.Areas.Identity.Pages.Student
{
    public class RegisterVerifyModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public RegisterVerifyModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public IList<Classes> Classes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Classes != null)
            {
                Classes = await _context.Classes.ToListAsync();
            }

            StudentsInClasses studclasTest = new StudentsInClasses();
            studclasTest.StudentID = HttpContext.Session.GetInt32("UserID").Value;
            studclasTest.ClassesID = 457;

            _context.StudentsInClasses.Add(studclasTest);
            await _context.SaveChangesAsync();

            //return RedirectToPage("./RegisterForClass");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }
            var classes = await _context.Classes.FindAsync(id);

            if (classes != null)
            {
                Classes = classes;

                StudentsInClasses studclasTest = new StudentsInClasses();
                //studclasTest.StudentID = HttpContext.Session.GetInt32("UserID").Value;
                studclasTest.StudentID = 600;
                studclasTest.ClassesID = 600;

                _context.StudentsInClasses.Add(studclasTest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./RegisterForClass");
        }

    }
}
