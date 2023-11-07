using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing.Text;
using System.Security;
using System.Security.Claims;





namespace Assignment1Attempt4TestAttempt2
{
    [TestClass]
    public class UnitTest1
    {

        //setting up a copy of the Assignment1Attempt4DBContext
        private static DbContextOptions<Assignment1Attempt4DBContext> options = new DbContextOptionsBuilder<Assignment1Attempt4DBContext>().UseInMemoryDatabase(databaseName: "mockDB").Options;
        private readonly Assignment1Attempt4DBContext Context = new Assignment1Attempt4DBContext(options);



        //Deletes then creates a new mockDB every time tests are ran
        [TestMethod]
        public void TestInit()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }


        [TestMethod]
        public void InstructorCanCreateCourseTest()
        {
            int profID = 1;


            int initalCourseCount = Context.Classes.Count();


            Classes testClass = new Classes
            {
                ProfessorID = profID,
                ClassName = "Test " + initalCourseCount,
                Department = "CS",
                Location = "Test " + initalCourseCount,
                PFName = "Test " + initalCourseCount,
                PLName = "Test " + initalCourseCount,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Monday = true,
                Tuesday = false,
                Wednesday = false,
                Thursday = true,
                Friday = false
            };

            Context.Classes.Add(testClass);
            Context.SaveChanges();

            int postCourseCount = Context.Classes.Count();

            Assert.AreNotEqual(initalCourseCount, postCourseCount);
        }

        //public void StudentCanRegisterForACourse()
        //{
        //    /*
        //     known stud id 

        //    x = # courses registered for

        //    use code to register student for any new course

        //     y = x + 1

        //    if true works 
        //    false bad
        //     */


        //}


        //public void InstructorCanCreateAssignemntTest()
        //{

        //}

        //public void StudentCanSubmitTextEntryTest()
        //{

        //}




        //Total 10 tests, 2 per person
    }
}