using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Assignment1Attempt4.Areas.Identity.Pages.Calendar
{
    public class CalendarModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public CalendarModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        public List<CalendarEvent> Events { get; set; }

        public class CalendarEvent
        {
            public string Title { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string ClassName { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public string PFName { get; set; }
            public string PLName { get; set; }
            public bool Monday { get; set; }
            public bool Tuesday { get; set; }
            public bool Wednesday { get; set; }
            public bool Thursday { get; set; }
            public bool Friday { get; set; }
        }

        public void OnGet()
        {
            Events = new List<CalendarEvent>();
            var classInfoList = _context.Classes.ToList();

            DateTime endDate = new DateTime(2023, 12, 14); // I guess you have to set it for a day after last desired day. 

            foreach (var classInfo in classInfoList)
            {
                var startTime = classInfo.StartTime;
                var endTime = classInfo.EndTime;
                if (classInfo.Monday)
                {
                    AddEvent(classInfo, startTime, endTime, DayOfWeek.Monday, endDate);
                }
                if (classInfo.Tuesday)
                {
                    AddEvent(classInfo, startTime, endTime, DayOfWeek.Tuesday, endDate);
                }
                if (classInfo.Wednesday)
                {
                    AddEvent(classInfo, startTime, endTime, DayOfWeek.Wednesday, endDate);
                }
                if (classInfo.Thursday)
                {
                    AddEvent(classInfo, startTime, endTime, DayOfWeek.Thursday, endDate);
                }
                if (classInfo.Friday)
                {
                    AddEvent(classInfo, startTime, endTime, DayOfWeek.Friday, endDate);
                }
            }
        }

        private void AddEvent(Assignment1Attempt4.Areas.Identity.Data.Model.Classes classInfo, DateTime startTime, DateTime endTime, DayOfWeek dayOfWeek, DateTime endDate)
        {
            DateTime eventStart = startTime;
            while (eventStart.DayOfWeek != dayOfWeek)
            {
                eventStart = eventStart.AddDays(1);
            }
            while (eventStart <= endDate)
            {
                var eventEnd = eventStart.AddHours((endTime - startTime).TotalHours);

                if (eventEnd <= endDate)
                {
                    Events.Add(new CalendarEvent
                    {
                        Title = classInfo.ClassName,
                        Start = eventStart,
                        End = eventEnd,
                        ClassName = "",
                        Department = classInfo.Department,
                        Location = classInfo.Location,
                        PFName = classInfo.PFName,
                        PLName = classInfo.PLName,
                        Monday = classInfo.Monday,
                        Tuesday = classInfo.Tuesday,
                        Wednesday = classInfo.Wednesday,
                        Thursday = classInfo.Thursday,
                        Friday = classInfo.Friday
                    });
                    eventStart = eventStart.AddDays(7);
                }
                else
                {
                    break;
                }
            }
        }

        public string GetEventsAsJson()
        {
            return JsonConvert.SerializeObject(Events);
        }
    }
}
