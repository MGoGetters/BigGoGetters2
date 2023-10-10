using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment1Attempt4.Areas.Identity.Data.Model
{
    public class Classes
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string Department {  get; set; }
        public string Location {  get; set; }
        public string PFName {  get; set; }
        public string PLName { get; set; }
        [DataType(DataType.Time)] 
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public Boolean Monday {  get; set; }
        public Boolean Tuesday { get; set; }
        public Boolean Wednesday { get; set; }
        public Boolean Thursday { get; set; }
        public Boolean Friday { get; set; }
        public int ProfessorID {  get; set; }

    }
}
