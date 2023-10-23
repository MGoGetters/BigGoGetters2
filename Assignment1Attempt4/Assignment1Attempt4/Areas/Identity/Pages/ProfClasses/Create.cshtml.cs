using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;

namespace Assignment1Attempt4.Areas.Identity.Pages.AssignmentPages
{
    public class CreateModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public CreateModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClassID"] = HttpContext.Session.GetInt32("ClassID").Value;
            return Page();
        }

        [BindProperty]
        public Assignments Assignments { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Assignments.ClassesID = HttpContext.Session.GetInt32("ClassID").Value;

            if (_context.Assignments == null || Assignments == null)
            {
                return Page();
            }

            _context.Assignments.Add(Assignments);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ProfCards");
        }
    }
}
