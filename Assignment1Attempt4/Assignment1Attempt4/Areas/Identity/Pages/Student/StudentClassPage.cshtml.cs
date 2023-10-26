using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1Attempt4.Areas.Identity.Pages.ProfClasses
{
    public class StudentClassPageModel : PageModel
    {
        
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public StudentClassPageModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public Classes Classes { get; set; } = default!;
        public Assignments Assignments { get; set; } = default!;
        public IList<Assignments> assignments { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes.FirstOrDefaultAsync(m => m.ID == id);
            assignments = _context.Assignments.Where(c => c.ClassesID == id).ToList();

            HttpContext.Session.SetInt32("ClassID", classes.ID);

			if (classes == null)
            {
                return NotFound();
            }
            else
            {
                Classes = classes;
            }
            return Page();
        }
    }
}
