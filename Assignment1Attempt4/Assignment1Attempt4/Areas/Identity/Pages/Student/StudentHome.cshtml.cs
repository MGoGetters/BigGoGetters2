using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1Attempt4.Areas.Identity.Pages.Student
{
    public class StudentHomeModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentHomeModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        //public IList<StudentsInClasses> StudentsInClasses { get; set; } = default!;

        public IList<Classes> Classes { get; set; } = default!;
        public IList<StudentsInClasses> StudentsInClasses { get; set; } = default!;

        public async Task OnGetAsync()
        {

            //if (_context.StudentsInClasses != null)
            //{
            //    StudentsInClasses = await _context.StudentsInClasses.ToListAsync();
            //}

            if (_context.Classes != null)
            {
                Classes = await _context.Classes.ToListAsync();


                int StudentID = HttpContext.Session.GetInt32("UserID").Value;

                var studentClasses = _context.StudentsInClasses.Where(sc => sc.StudentID == StudentID).Select(sc => sc.ClassesID);
                Classes = _context.Classes.Where(c => studentClasses.Contains(c.ID)).ToList();

            }
        }
    }
}
