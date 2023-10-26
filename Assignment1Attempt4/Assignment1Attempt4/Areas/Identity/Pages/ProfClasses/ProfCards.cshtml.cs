using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment1Attempt4.Areas.Identity.Pages.ProfClasses
{
    public class DashboardCardsModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public DashboardCardsModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public IList<Classes> Classes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Classes != null)
            {
                //Classes = await _context.Classes.ToListAsync();

                int ProfessorIDtest = HttpContext.Session.GetInt32("UserID").Value;
                Classes = _context.Classes.Where(c => c.ProfessorID == ProfessorIDtest).ToList();
            }
        }

    }
}
