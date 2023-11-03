using Assignment1Attempt4.Areas.Identity.Data;
using Assignment1Attempt4.Controllers;
using Assignment1Attempt4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment1Attempt4.Areas.Identity.Pages.ProfileTestPage
{
    public class ProfileModel : PageModel
    {

        private readonly CheckoutApiController _checkoutController;
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;
        private readonly ILogger<CheckoutApiController> _logger;
        private readonly UserManager<Assignment1Attempt4User> _userManager;

        public ProfileModel(CheckoutApiController checkoutController, Assignment1Attempt4DBContext context, ILogger<CheckoutApiController> logger, UserManager<Assignment1Attempt4User> userManager)
        {
            _checkoutController = checkoutController;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public void OnGet()
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
        }
    }
}
