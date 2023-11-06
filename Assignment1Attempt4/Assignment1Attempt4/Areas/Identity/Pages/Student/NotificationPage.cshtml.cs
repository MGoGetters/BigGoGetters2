using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1Attempt4.Areas.Identity.Pages.Student
{
    public class NotificationPageModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public NotificationPageModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public Assignments Assignments { get; set; } = default!;
        public List<StudentSubmitsAssignment> StudentSubmissions { get; set; } = new List<StudentSubmitsAssignment>();

        public IList<StudentSubmitsAssignment> assignmentLists { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
                var userID = HttpContext.Session.GetInt32("UserID").Value;

                assignmentLists = _context.StudentSubmitsAssignment.Where(c => c.IsGraded == true).Where(m =>  userID == m.studentID).ToList();

                return Page();
        }
    }
}
