namespace HighPaw.Web.Controllers
{
    using HighPaw.Data;
    using HighPaw.Web.Models;
    using HighPaw.Web.Models.Pets;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly HighPawDbContext data;

        public HomeController(HighPawDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var allPets = this.data
                .Pets
                .Select(p => new PetListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    ImageUrl = p.ImageUrl,
                    Shelter = p.Shelter.Address
                })
                .OrderBy(p => p.Id)
                .Take(4)
                .ToList();

            return View(allPets);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}