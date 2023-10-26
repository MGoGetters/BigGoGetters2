using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;
using Assignment1Attempt4.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;

namespace Assignment1Attempt4.Areas.Identity.Pages.Student
{
    public class StudentCardsModel : PageModel
    {

        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentCardsModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        //public IList<StudentsInClasses> StudentsInClasses { get; set; } = default!;

        public IList<Assignments> Assignments { get; set; } = default!;
        public IList<Classes> Classes { get; set; } = default!;
        public IList<StudentsInClasses> StudentsInClasses { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Classes != null)
            {
                Classes = await _context.Classes.ToListAsync();


                int StudentID = HttpContext.Session.GetInt32("UserID").Value;

                var studentClasses = _context.StudentsInClasses.Where(sc => sc.StudentID == StudentID).Select(sc => sc.ClassesID);
                Classes = _context.Classes.Where(c => studentClasses.Contains(c.ID)).ToList();

            }

            if (_context.Assignments != null)
            {
                Assignments = await _context.Assignments.ToListAsync();
               

                DateTime currentDate = DateTime.Now; // Get the current date and time
                Assignments = _context.Assignments
                    .Where(a => a.DueDate >= currentDate) // Filter assignments with a due date greater than or equal to the current date
                    .OrderBy(a => a.DueDate) // or OrderByDescending for descending order
                    .Take(5)
                    .ToList();
            }
        }
    }
}
