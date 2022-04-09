namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Admin;
    using HighPaw.Services.Admin.Models;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IAdminService service;

        public HomeController(IAdminService service)
            => this.service = service;

        public IActionResult Index()
        {
            var model = new PetArticleServiceModel
            {
                Pets = this.service.GetLatestPets(),
                Articles = this.service.GetLatestArticles()
            };

            return View(model);
        }

    }
}
