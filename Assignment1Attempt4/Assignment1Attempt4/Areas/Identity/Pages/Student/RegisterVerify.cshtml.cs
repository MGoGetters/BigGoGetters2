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

        public Classes Classes { get; set; } = default!;
        public IList<Classes> Assoc_Class { get; set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            /*if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes.FirstOrDefaultAsync(m => m.ID == id);
            if (classes == null)
            {
                return NotFound();
            }
            else
            {
                Classes = classes;
            }
            return Page();*/

            if (_context.Classes != null)
            {
                Assoc_Class = await _context.Classes.ToListAsync();
            }

            StudentsInClasses studclasTest = new StudentsInClasses();
            studclasTest.StudentID = HttpContext.Session.GetInt32("UserID").Value;
            studclasTest.ClassesID = 707;

            _context.StudentsInClasses.Add(studclasTest);
            await _context.SaveChangesAsync();
        }

    }
}
