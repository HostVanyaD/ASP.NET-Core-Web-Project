namespace HighPaw.Web.Controllers
{
    using HighPaw.Data;
    using HighPaw.Web.Models.Pets;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    using static Areas.Admin.AdminConstants;

    public class HomeController : Controller
    {
        private readonly HighPawDbContext data;

        public HomeController(HighPawDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            var allPets = this.data
                .Pets
                .Select(p => new PetListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    Gender = p.Gender,
                    ImageUrl = p.ImageUrl,
                    Shelter = p.Shelter.Address
                })
                .OrderBy(p => p.Id)
                .Take(3)
                .ToList();

            return View(allPets);
        }

        public IActionResult Work()
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}