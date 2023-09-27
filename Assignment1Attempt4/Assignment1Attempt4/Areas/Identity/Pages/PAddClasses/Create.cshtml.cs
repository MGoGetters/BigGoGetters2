using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;

namespace Assignment1Attempt4.Areas.Identity.Pages.PAddClasses
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
            return Page();
        }

        [BindProperty]
        public Classes Classes { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            int testDate = DateTime.Compare(Classes.StartTime, Classes.EndTime);

            //if start time is later than end time it returns a 1
            if (testDate > 0)
            {
                ModelState.AddModelError("Classes.StartTime", "Start Time cannot be later than End Time");
            }

            testDate = DateTime.Compare(Classes.EndTime, Classes.StartTime);

            //if end time is earlier than start time it returns a -1
            if (testDate < 0)
            {
                ModelState.AddModelError("Classes.EndTime", "End Time cannot be earlier than Start Time");
            }

          if (!ModelState.IsValid || _context.Classes == null || Classes == null)
            {
                return Page();
            }

            _context.Classes.Add(Classes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
