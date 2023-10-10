using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment1Attempt4.Areas.Identity.Data.Model
{
    public class StudentsInClasses
    {
        public int ID { get; set; }

        [ForeignKey("Classes")]
        public int ClassesID { get; set; }

        [ForeignKey("AspNetUsers")]
        public int StudentID { get; set; }
    }
}
