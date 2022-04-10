namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Pet;
    using HighPaw.Services.Pet.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
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

        public IActionResult HowToAdopt()
            => View();

        public IActionResult About()
            => View();

        public IActionResult Contact()
            => View();

        public IActionResult Quiz()
            => View();

        public IActionResult Results([FromQuery] PetQueryServiceModel query)
        {
            var queryResult = this.pets.GetQuizResults(
                query.CurrentPage,
                query.PageSize,
                query.Filters);

            query.TotalItems = queryResult.TotalItems;
            query.Items = queryResult.Items;

            return View(query);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}