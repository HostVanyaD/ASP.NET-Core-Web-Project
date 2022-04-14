namespace HighPaw.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using HighPaw.Services.Pet;
    using HighPaw.Services.Pet.Models;
    using HighPaw.Services.Article;
    using static Areas.Admin.AdminConstants;
    using HighPaw.Web.Models.Home;

    public class HomeController : Controller
    {
        private readonly IPetService pets;
        private readonly IArticleService articles;

        public HomeController(
            IPetService pets,
            IArticleService articles)
        {
            this.pets = pets;
            this.articles = articles;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            var latestPets = this.pets.Latest();
            var latestArticles = this.articles.Latest();

            var listing = new PetArticleListingViewModel
            {
                LatestPets = latestPets,
                LatestArticles = latestArticles
            };

            return View(listing);
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