using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment1Attempt4.Areas.Identity.Data.Model
{
    public class StudentSubmitsAssignment
    {
        public int ID { get; set; }
        public string AssignmentName { get; set; }
        public float StudentGrade { get; set; }
        public float GradeTotal { get; set; }
        [ForeignKey("Assignments")]
        public int assignmentID { get; set; }
        [ForeignKey("AspNetUsers")]
        public int studentID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
    }
}
