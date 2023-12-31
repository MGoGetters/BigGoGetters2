﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1Attempt4.Areas.Identity.Data.Model;
using Assignment1Attempt4.Data;

namespace Assignment1Attempt4.Areas.Identity.Pages.PAddClasses
{
    public class EditModel : PageModel
    {
        private readonly Assignment1Attempt4.Data.Assignment1Attempt4DBContext _context;

        public EditModel(Assignment1Attempt4.Data.Assignment1Attempt4DBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Classes Classes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classes =  await _context.Classes.FirstOrDefaultAsync(m => m.ID == id);
            if (classes == null)
            {
                return NotFound();
            }
            Classes = classes;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int testDate = DateTime.Compare(Classes.StartTime, Classes.EndTime);

            Classes.ProfessorID = HttpContext.Session.GetInt32("UserID").Value;
            Classes.PFName = HttpContext.Session.GetString("CurrentFName");
            Classes.PLName = HttpContext.Session.GetString("CurrentLName");

            //if start time is later than end time it returns a 1
            if (testDate > 0)
            {
                ModelState.AddModelError("Classes.StartTime", "Start Time cannot be later than End Time");
            }

            testDate = DateTime.Compare(Classes.EndTime, Classes.StartTime);

            //if end time is earlier than start time it returns a -1
            if (testDate < 0)
            {
                ModelState.AddModelError("Classes.EndTime", "End Time cannot be earlier than Start Time");
            }

            if (_context.Classes == null || Classes == null)
            {
                return Page();
            }

            _context.Attach(Classes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassesExists(Classes.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClassesExists(int id)
        {
          return (_context.Classes?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
