namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Pet;
    using Microsoft.AspNetCore.Mvc;

    using static Areas.Admin.AdminConstants;

    public class HomeController : Controller
    {
        private readonly IPetService pets;

        public HomeController(IPetService pets)
            => this.pets = pets;

        public IActionResult Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            var latestPets = this.pets.Latest();

            return View(latestPets);
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