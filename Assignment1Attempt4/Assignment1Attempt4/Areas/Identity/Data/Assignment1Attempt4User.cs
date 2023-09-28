using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Assignment1Attempt4.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Assignment1Attempt4User class
public class Assignment1Attempt4User : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; }

    [PersonalData]
    [Column(TypeName = "DateTime")]
    public DateTime BirthDate { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string profOrStudent { get; set; }
}

