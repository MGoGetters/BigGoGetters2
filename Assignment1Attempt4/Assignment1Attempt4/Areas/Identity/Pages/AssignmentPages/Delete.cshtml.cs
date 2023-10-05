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
    public class DeleteModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public DeleteModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Assignments Assignments { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assignments == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments.FirstOrDefaultAsync(m => m.ID == id);

            if (assignments == null)
            {
                return NotFound();
            }
            else 
            {
                Assignments = assignments;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Assignments == null)
            {
                return NotFound();
            }
            var assignments = await _context.Assignments.FindAsync(id);

            if (assignments != null)
            {
                Assignments = assignments;
                _context.Assignments.Remove(Assignments);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
