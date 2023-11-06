using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Assignment1Attempt4.Data;
using Assignment1Attempt4.Models;
using Assignment1Attempt4.Areas.Identity.Data;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Assignment1Attempt4.Areas.Identity.Pages.Account.AssignmentGrade
{
    public class AssignmentGradeModel : PageModel
    {
        private readonly ILogger<AssignmentGradeModel> _logger;
        private readonly Assignment1Attempt4DBContext _context;

        [BindProperty]
        public int AssignmentID { get; set; }

        [BindProperty]
        public double Grade { get; set; }

        [BindProperty]
        public double MaxPoints { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string AssignmentName { get; set; }

        [BindProperty]
        public string studentID { get; set; }

        [BindProperty]
        public string IsGraded { get; set; }

        public AssignmentGradeModel(ILogger<AssignmentGradeModel> logger, Assignment1Attempt4DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int assignmentID = Convert.ToInt32(Request.Form["assignmentID"]);
            double grade = Convert.ToDouble(Request.Form["grade"]);
            double maxPoints = Convert.ToDouble(Request.Form["maxPoints"]);
            string firstName = Request.Form["firstName"];
            string lastName = Request.Form["lastName"];
            string assignmentName = Request.Form["assignmentName"];
            bool isGraded = true;

            if (grade < 0 || grade > maxPoints)
            {
                ModelState.AddModelError("grade", "Invalid grade. Please enter a valid grade within the allowed range.");
                return Page();
            }

            var user = await _context.Users
                .Where(u => u.FirstName == firstName && u.LastName == lastName)
                .Select(u => new { u.Id, u.UserNumber })
                .FirstOrDefaultAsync();

            if (user != null)
            {
                int userNumber = user.UserNumber;

                var studentAssignment = new StudentSubmitsAssignment
                {
                    AssignmentName = assignmentName,
                    StudentGrade = (float)grade,
                    GradeTotal = (float)maxPoints,
                    assignmentID = assignmentID,
                    studentID = userNumber,
                    Fname = firstName,
                    Lname = lastName,
                    IsGraded = isGraded
                };



                _context.StudentSubmitsAssignment.Add(studentAssignment);
                await _context.SaveChangesAsync();

                return Page();
            }
            else
            {
                ModelState.AddModelError("firstName", "User not found. Please check the first and last name.");
                return Page();
            }
        }
    }
}
