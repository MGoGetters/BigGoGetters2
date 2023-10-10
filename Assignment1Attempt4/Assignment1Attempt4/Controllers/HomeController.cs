using Assignment1Attempt4.Areas.Identity.Data;
using Assignment1Attempt4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assignment1Attempt4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Assignment1Attempt4User> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Assignment1Attempt4User> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        /*public IActionResult Index()
        {
            ViewData["UserID"]=_userManager.GetUserId(this.User);
            return View();
        }*/

        public IActionResult Index()
        {
            /*ViewData["UserID"] = _userManager.GetUserId(this.User);
            ViewData["FirstName"] = _userManager.GetUserId(this.User);
            ViewData["LastName"] = _userManager.GetUserId(this.User);*/

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
            return View();
        }

        public IActionResult ProfessorHome()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            ViewData["FirstName"] = _userManager.GetUserId(this.User);
            ViewData["LastName"] = _userManager.GetUserId(this.User);

            var userid = _userManager.GetUserId(this.User);
            var myUser = _userManager.Users.Where(x => x.Id == userid).ToList()[0];

            ViewData["UserID"] = myUser.UserName;
            ViewData["FirstName"] = myUser.FirstName;
            ViewData["LastName"] = myUser.LastName;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}