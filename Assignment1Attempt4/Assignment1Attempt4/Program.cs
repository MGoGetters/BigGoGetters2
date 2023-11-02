using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Assignment1Attempt4.Data;
using Assignment1Attempt4.Areas.Identity.Data;
using Stripe;
using Assignment1Attempt4.Controllers;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Assignment1Attempt4DBContextConnection") ?? throw new InvalidOperationException("Connection string 'Assignment1Attempt4DBContextConnection' not found.");

builder.Services.AddDbContext<Assignment1Attempt4DBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Assignment1Attempt4User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Assignment1Attempt4DBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//makes session work, otherwise breaks everything DO NOT TOUCH UNLESS YOU KNOW WHAT YOUR DOING (I sure dont)
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<CheckoutApiController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//gets stripe SecretKey
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseSession();

app.Run();