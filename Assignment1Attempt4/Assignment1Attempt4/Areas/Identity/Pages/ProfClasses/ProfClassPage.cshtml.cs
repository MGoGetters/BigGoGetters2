using Assignment1Attempt4.Areas.Identity.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment1Attempt4.Areas.Identity.Pages.ProfClasses
{
    public class ProfClassPageModel : PageModel
    {
        
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public ProfClassPageModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }
        public Classes Classes { get; set; } = default!;
        public Assignments Assignments { get; set; } = default!;
        public IList<Assignments> assignments { get; set; } = default!;
        public int TotalMaxPoints { get; set; }
        public List<int> AssignmentIDs { get; set; } = new List<int>();

        public List<StudentSubmitsAssignment> StudentSubmissions { get; set; } = new List<StudentSubmitsAssignment>();

        public List<StudentSum> StudentSums { get; set; } = new List<StudentSum>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes.FirstOrDefaultAsync(m => m.ID == id);
            assignments = _context.Assignments.Where(c => c.ClassesID == id).ToList();


            TotalMaxPoints = assignments.Sum(a => a.MaxPoints);

            AssignmentIDs = assignments.Select(a => a.ID).ToList();

            StudentSubmissions = _context.StudentSubmitsAssignment
                .Where(s => AssignmentIDs.Contains(s.assignmentID))
                .OrderBy(s => s.studentID)
                .ToList();

            StudentSums = StudentSubmissions
        .GroupBy(s => s.studentID)
        .Select(group => new StudentSum
        {
            StudentID = group.Key,
            SumOfStudentGrade = group.Sum(s => s.StudentGrade),
            SumOfGradeTotal = group.Sum(s => s.GradeTotal),
            FinalGrade = group.Sum(s => s.StudentGrade) / (double)group.Sum(s => s.GradeTotal)
        })
        .ToList();
            int countA = 0;
            int countAMinus = 0;
            int countBPlus = 0;
            int countB = 0;
            int countBMinus = 0;
            int countCPlus = 0;
            int countC = 0;
            int countCMinus = 0;
            int countDPlus = 0;
            int countD = 0;
            int countDMinus = 0;
            int countE = 0;

            foreach (var studentSum in StudentSums)
            {
                double finalPercentage = studentSum.FinalGrade * 100;

                switch (finalPercentage)
                {
                    case var _ when finalPercentage >= 94.00:
                        studentSum.LetterGrade = "A";
                        countA++;
                        break;
                    case var _ when finalPercentage >= 90.00:
                        studentSum.LetterGrade = "A-";
                        countAMinus++;
                        break;
                    case var _ when finalPercentage >= 87.00:
                        studentSum.LetterGrade = "B+";
                        countBPlus++;
                        break;
                    case var _ when finalPercentage >= 84.00:
                        studentSum.LetterGrade = "B";
                        countB++;
                        break;
                    case var _ when finalPercentage >= 80.00:
                        studentSum.LetterGrade = "B-";
                        countBMinus++;
                        break;
                    case var _ when finalPercentage >= 77.00:
                        studentSum.LetterGrade = "C+";
                        countCPlus++;
                        break;
                    case var _ when finalPercentage >= 74.00:
                        studentSum.LetterGrade = "C";
                        countC++;
                        break;
                    case var _ when finalPercentage >= 70.00:
                        studentSum.LetterGrade = "C-";
                        countCMinus++;
                        break;
                    case var _ when finalPercentage >= 67.00:
                        studentSum.LetterGrade = "D+";
                        countDPlus++;
                        break;
                    case var _ when finalPercentage >= 64.00:
                        studentSum.LetterGrade = "D";
                        countD++;
                        break;
                    case var _ when finalPercentage >= 60.00:
                        studentSum.LetterGrade = "D-";
                        countDMinus++;
                        break;
                    default:
                        studentSum.LetterGrade = "E";
                        countE++;
                        break;
                }
            }
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

    public class StudentSum
    {
        public int StudentID { get; set; }
        public float SumOfStudentGrade { get; set; }
        public float SumOfGradeTotal { get; set; }
        public double FinalGrade { get; set; }
        public string LetterGrade { get; set; }

    }
}
