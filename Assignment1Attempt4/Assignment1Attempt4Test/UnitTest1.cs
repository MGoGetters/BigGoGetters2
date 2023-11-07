using Assignment1Attempt4.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing.Text;
using System.Security;
using System.Security.Claims;




namespace Assignment1Attempt4Test
{
    [TestClass]
    public class UnitTest1
    {

        private static DbContextOptions<Assignment1Attempt4DBContext> options = new DbContextOptionsBuilder<Assignment1Attempt4DBContext>().UseInMemoryDatabase(databaseName: "mockDB").Options;
        private readonly Assignment1Attempt4DBContext Context = new Assignment1Attempt4DBContext(options);





        [TestMethod]
        public void TestInit()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }


        [TestMethod]
        public void InstructorCanCreateCourseTest()
        {
            /*
            known intr id  = 32
            x = current # courses

            use code to create a new course for instID == 32

            y = x + 1

            if true then works
            if false then bad :(

             */
            int initalCourseCount = Context.Database.Count();



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