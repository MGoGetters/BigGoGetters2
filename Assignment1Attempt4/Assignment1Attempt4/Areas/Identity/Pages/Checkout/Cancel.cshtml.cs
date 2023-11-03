using Assignment1Attempt4.Areas.Identity.Data;
using Assignment1Attempt4.Controllers;
using Assignment1Attempt4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment1Attempt4.Areas.Identity.Pages.Checkout
{
    public class CancelModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;
        private readonly UserManager<Assignment1Attempt4User> _userManager;



        public CancelModel(UserManager<Assignment1Attempt4User> userManager, Assignment1Attempt4DBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string stuOrProf { get; set; }

        public void OnGet()
        {
            stuOrProf = HttpContext.Session.GetString("StuOrProf");
        }

    }
}
