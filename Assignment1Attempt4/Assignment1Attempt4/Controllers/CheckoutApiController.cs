using System.Collections.Generic;
using Assignment1Attempt4.Areas.Identity.Data;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Controllers;
using Assignment1Attempt4.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;
using Microsoft.AspNetCore.Http;

public class StripeOptions
{
    public string option { get; set; }
}

namespace Assignment1Attempt4.Controllers
{

    //I dont think we need 'Program' or 'Startup'
    //not deleting them in case I am wrong

    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        WebHost.CreateDefaultBuilder(args)
    //          .UseUrls("http://0.0.0.0:4242")
    //          .UseWebRoot("public")
    //          .UseStartup<Startup>()
    //          .Build()
    //          .Run();
    //    }
    //}

    //public class Startup
    //{
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddControllers().AddNewtonsoftJson();
    //    }
    //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //    {
    //        // This is a public sample test API key.
    //        // Don’t submit any personally identifiable information in requests made with this key.
    //        // Sign in to see your own test API key embedded in code samples.
    //        StripeConfiguration.ApiKey = "sk_test_51O1A5TFvCFi2Xa6Y26tS2zzjoHlPUwIIwXhBpR32OihbP6nsfJ32vxcrO8B1Z4dVmq0pxnDHuxAwQfqLsMc9Dmo1009EHdMiYo";
    //        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
    //        app.UseRouting();
    //        app.UseStaticFiles();
    //        app.UseEndpoints(endpoints => endpoints.MapControllers());
    //    }
    //}

    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {


        private readonly ILogger<CheckoutApiController> _logger;
        private readonly UserManager<Assignment1Attempt4User> _userManager;
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;


        public CheckoutApiController(ILogger<CheckoutApiController> logger, UserManager<Assignment1Attempt4User> userManager)
        {
            _logger = logger;
            _userManager = userManager;

        }

        public IActionResult Index()
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
            return View();
        }


        //private int GetNumCredits(int StudentID)
        //{
        //    //we should probably handle exceptions lmao
        //    return 4 * _context.StudentsInClasses.Where(sc => sc.StudentID == StudentID).Select(sc => sc.StudentID).Sum();
        //    //int numClasses = _context.StudentsInClasses.Where(sc => sc.StudentID == StudentID).Select(sc => sc.StudentID).Sum();
        //    //int numCredits = 4 * numClasses;
        //    //return numCredits;


        //}


        [HttpPost]
        public ActionResult Create()
        {
            
            //int numCredits = GetNumCredits((int)HttpContext.Session.GetInt32("UserID"));



            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
            {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = 10000,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Tuiton",
                },
                },
                //Quantity = numCredits,
                Quantity = 4,
            },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:7177/success.html",
                CancelUrl = "http://localhost:7177/cancel.html",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }



        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            //this was the original sample code
            //not deleting incase we need it 

            //    var options = new SessionCreateOptions
            //    {
            //        LineItems = new List<SessionLineItemOptions>
            //{
            //  new SessionLineItemOptions
            //  {
            //    PriceData = new SessionLineItemPriceDataOptions
            //    {
            //      UnitAmount = 2000,
            //      Currency = "usd",
            //      ProductData = new SessionLineItemPriceDataProductDataOptions
            //      {
            //        Name = "T-shirt",
            //      },
            //    },
            //    Quantity = 1,
            //  },
            //},
            //        Mode = "payment",
            //        SuccessUrl = "http://localhost:4242/success.html",
            //        CancelUrl = "http://localhost:4242/cancel.html",
            //    };

            //    var service = new SessionService();
            //    Session session = service.Create(options);

            //    Response.Headers.Add("Location", session.Url);
            //    return new StatusCodeResult(303);



            //int numCredits = GetNumCredits((int)HttpContext.Session.GetInt32("UserID"));
            //int numCredits = GetNumCredits((int)HttpContext.Session.GetInt32("UserID"));



            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
            {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = 10000,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Tuiton",
                },
                },
                //Quantity = numCredits,
                Quantity = 4,

            },
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:7177/success.html",
                CancelUrl = "http://localhost:7177/cancel.html",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }


}
