using Assignment1Attempt4.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment1Attempt4.Areas.Identity.Data.Model;

namespace Assignment1Attempt4.Data;

public class Assignment1Attempt4DBContext : IdentityDbContext<Assignment1Attempt4User>
{
    public Assignment1Attempt4DBContext(DbContextOptions<Assignment1Attempt4DBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Assignment1Attempt4.Areas.Identity.Data.Model.StudentsInClasses> StudentsInClasses { get; set; } = default!;
    public DbSet<Assignment1Attempt4.Areas.Identity.Data.Model.Classes> Classes { get; set; } = default!;

    public DbSet<Assignment1Attempt4.Areas.Identity.Data.Model.Assignments> Assignments { get; set; } = default!;
}
