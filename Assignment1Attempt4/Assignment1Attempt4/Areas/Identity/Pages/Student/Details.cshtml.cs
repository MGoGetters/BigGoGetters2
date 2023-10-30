using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;

namespace Assignment1Attempt4.Areas.Identity.Pages.AssignmentPages
{
    public class StudentDetailsModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public StudentDetailsModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public Assignments Assignments { get; set; } = default!;
        public List<StudentSubmitsAssignment> StudentSubmissions { get; set; } = new List<StudentSubmitsAssignment>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (int.TryParse(Request.Query["id"], out int assignmentID))
            {
                ViewData["ClassID"] = HttpContext.Session.GetInt32("ClassID").Value;

                if (_context.Assignments == null)
                {
                    return NotFound();
                }

                var assignments = await _context.Assignments.FirstOrDefaultAsync(m => m.ID == assignmentID);
                if (assignments == null)
                {
                    return NotFound();
                }
                else
                {
                    Assignments = assignments;
                }

                StudentSubmissions = await _context.StudentSubmitsAssignment
                    .Where(submission => submission.assignmentID == assignmentID)
                    .ToListAsync();

                return Page();
            }

            return NotFound();
        }
    }
}
