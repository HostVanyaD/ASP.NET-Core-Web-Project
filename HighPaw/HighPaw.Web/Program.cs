using HighPaw.Data;
using HighPaw.Data.Models;
using HighPaw.Services.Admin;
using HighPaw.Services.Article;
using HighPaw.Services.Event;
using HighPaw.Services.Pet;
using HighPaw.Services.Shelter;
using HighPaw.Services.Volunteer;
using HighPaw.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder
    .Services
    .AddDbContext<HighPawDbContext>(options =>
        options.UseSqlServer(connectionString))
    .AddDatabaseDeveloperPageExceptionFilter()
    .AddDefaultIdentity<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HighPawDbContext>();

builder
    .Services
    .AddMemoryCache();

builder
    .Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    });

builder
    .Services
    .AddAutoMapper(typeof(Program));

// App services
builder
    .Services
    .AddTransient<IAdminService, AdminService>()
    .AddTransient<IPetService, PetService>()
    .AddTransient<IArticleService, ArticleService>()
    .AddTransient<IShelterService, ShelterService>()
    .AddTransient<IVolunteerService, VolunteerService>()
    .AddTransient<IEventService, EventService>();

var app = builder.Build();

app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
