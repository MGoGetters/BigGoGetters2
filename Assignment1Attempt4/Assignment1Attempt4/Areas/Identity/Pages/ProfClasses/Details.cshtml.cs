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
    public class DetailsModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public DetailsModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public int ClassID { get; set; }
        public Assignments Assignments { get; set; } = default!;
        public List<StudentSubmitsAssignment> StudentSubmitsAssignments { get; set; } = new List<StudentSubmitsAssignment>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ClassID = HttpContext.Session.GetInt32("ClassID").Value;

            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments.FirstOrDefaultAsync(m => m.ID == id);
            if (assignments == null)
            {
                return NotFound();
            }

            Assignments = assignments;
            StudentSubmitsAssignments = await _context.StudentSubmitsAssignment
                .Where(ssa => ssa.assignmentID == id)
                .ToListAsync();

            return Page();
        }
    }
}
