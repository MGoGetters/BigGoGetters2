using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;

namespace Assignment1Attempt4.Areas.Identity.Pages.AssignmentPages
{
    public class EditModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public EditModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignments Assignments { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["ClassID"] = HttpContext.Session.GetInt32("ClassID").Value;

            if (id == null || _context.Assignments == null)
            {
                return NotFound();
            }

            var assignments =  await _context.Assignments.FirstOrDefaultAsync(m => m.ID == id);
            if (assignments == null)
            {
                return NotFound();
            }
            Assignments = assignments;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Assignments.ClassesID = HttpContext.Session.GetInt32("ClassID").Value;

            if (_context.Assignments == null || Assignments == null)
            {
                return Page();
            }

            _context.Attach(Assignments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignmentsExists(Assignments.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ProfCards");
        }

        private bool AssignmentsExists(int id)
        {
          return (_context.Assignments?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
