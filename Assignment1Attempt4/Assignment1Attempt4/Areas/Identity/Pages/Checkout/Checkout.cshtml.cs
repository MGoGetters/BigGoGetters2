using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment1Attempt4.Controllers;
using Assignment1Attempt4.Data;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Assignment1Attempt4.Areas.Identity.Data;

namespace Assignment1Attempt4.Areas.Identity.Pages.Checkout
{
    public class CheckoutModel : PageModel
    {
        private readonly CheckoutApiController _checkoutController;
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;
        private readonly ILogger<CheckoutApiController> _logger;
        private readonly UserManager<Assignment1Attempt4User> _userManager;



        public CheckoutModel(CheckoutApiController checkoutController, Assignment1Attempt4DBContext context, ILogger<CheckoutApiController> logger, UserManager<Assignment1Attempt4User> userManager)
        {
            _checkoutController = checkoutController;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public IList<StudentsInClasses> StudentsInClasses { get; set; } = default!;
        public int moneyOwed { get; set; }
        public int numCredits { get; set; }


        public async Task OnGetAsync()
        {
            var userid = _userManager.GetUserId(this.User);
            var myUser = _userManager.Users.Where(x => x.Id == userid).ToList()[0];

            HttpContext.Session.SetString("CurrentFName", myUser.FirstName);
            HttpContext.Session.SetString("CurrentLName", myUser.LastName);
            HttpContext.Session.SetString("StuOrProf", myUser.profOrStudent);
            HttpContext.Session.SetInt32("UserID", myUser.UserNumber);

            ViewData["UserID"] = myUser.UserName;
            ViewData["FirstName"] = HttpContext.Session.GetString("CurrentFName");
            ViewData["LastName"] = HttpContext.Session.GetString("CurrentLName");
            ViewData["StuOrProf"] = HttpContext.Session.GetString("StuOrProf");
            


            if (_context.StudentsInClasses != null)
            {
                StudentsInClasses = await _context.StudentsInClasses.ToListAsync();

                int StudentID = HttpContext.Session.GetInt32("UserID").Value;
                //int StudentID = HttpContext.Session.GetInt32("UserID").Value;
                numCredits = 4 * _context.StudentsInClasses.Where(sc => sc.StudentID == StudentID).Select(sc => sc.StudentID).Count();

                moneyOwed = 100 * numCredits;
            }




        }
    }
}
