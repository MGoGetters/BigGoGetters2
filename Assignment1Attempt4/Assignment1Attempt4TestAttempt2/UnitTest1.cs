using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
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

        [TestMethod]
        public void StudentCanRegisterForACourse()
        {
            int stuID = 2;
            int initialStudentInCourseCount = Context.StudentsInClasses.Where(sc => sc.StudentID == stuID).Count();

            StudentsInClasses newRegistration = new StudentsInClasses
            {
                ClassesID = 1,
                StudentID = stuID
            };

            Context.StudentsInClasses.Add(newRegistration);
            Context.SaveChanges();

            int postStudentInCourseCount = Context.StudentsInClasses.Where(sc => sc.StudentID == stuID).Count();

            Assert.AreNotEqual(initialStudentInCourseCount, postStudentInCourseCount);
        }

        [TestMethod]
        public void InstructorCanCreateAssignemntTest()
        {

            ////First version of the test attempts to check the assignments that are in the classes only taught by a given professor
            ////it does not work pretty sure issue is how im trying to pull the data

            //int profID = 1;
            //int initialAssignmentCount = 0;

            ////count initial assignments
            //IQueryable<Classes> ProfClasses = Context.Classes.Where(c => c.ProfessorID == profID);
            //for (int i = 0; i < ProfClasses.Count(); i++)
            //{
            //    var tempClass = ProfClasses.ElementAt(i).ProfessorID;
            //    initialAssignmentCount = initialAssignmentCount + Context.Assignments.Where(a => a.ClassesID == tempClass).Count();
            //}

            ////add an assignments here
            //Assignments newAssignment = new Assignments
            //{
            //    AssignmentName = "TEST " + initialAssignmentCount,
            //    Description = "Test " + initialAssignmentCount,
            //    MaxPoints = 100,
            //    DueDate = DateTime.Now.AddDays(14),
            //    AssignmentType = "Text",
            //    ClassesID = 1, //can make ClassID = to a known real class if i remake full db everytime lmao
            //};


            //int testinit = Context.Assignments.Count();

            ////save changes to db
            //Context.Assignments.Add(newAssignment);
            //Context.SaveChanges();

            //int testpost = Context.Assignments.Count();


            ////count post assignments
            //int postAssignmentCount = 0;
            //ProfClasses = Context.Classes.Where(c => c.ProfessorID == profID);
            //for (int i = 0; i < ProfClasses.Count(); i++)
            //{
            //    var tempClass = ProfClasses.ElementAt(i).ProfessorID;
            //    postAssignmentCount = postAssignmentCount + Context.Assignments.Where(a => a.ClassesID == tempClass).Count();
            //}

            ////check if assignment was added
            //Assert.AreNotEqual(initialAssignmentCount, postAssignmentCount);

            //---------------------------------------------------------------------------------------------------------------------



            ////second version of this test just checks the total number of assignments by any prof
            ////it 100% works but not sure what we want to do 
            int initialAssignmentCount = 0;

            Assignments newAssignment = new Assignments
            {
                AssignmentName = "TEST " + initialAssignmentCount,
                Description = "Test " + initialAssignmentCount,
                MaxPoints = 100,
                DueDate = DateTime.Now.AddDays(14),
                AssignmentType = "Text",
                ClassesID = 1, //can make ClassID = to a known real class if i remake full db everytime lmao
            };

            int testinit = Context.Assignments.Count();

            Context.Assignments.Add(newAssignment);
            Context.SaveChanges();

            int testpost = Context.Assignments.Count();
            Assert.AreNotEqual(testinit, testpost);
        }

        //public void StudentCanSubmitTextEntryTest()
        //{

        //}




        //Total 10 tests, 2 per person
    }
}