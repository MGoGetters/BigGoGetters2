using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Assignment1Attempt4.Areas.Identity.Data.Model
{
    public class Assignments
    {
        public int ID { get; set; }
        public string AssignmentName { get; set; }
        public string Description { get; set; }
        public int MaxPoints { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        public string AssignmentType { get; set; }

        [ForeignKey("Classes")]
        public int ClassesID { get; set; }
    }
}
