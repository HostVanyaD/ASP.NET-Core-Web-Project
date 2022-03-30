namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Shelter;
    using Microsoft.AspNetCore.Mvc;

    public class SheltersController : BaseController
    {
        private readonly IShelterService shelters;

        public SheltersController(IShelterService shelters)
            => this.shelters = shelters;

        public IActionResult All()
        {
            var allShelters = this.shelters.GetAll();

            return View(allShelters);
        }
    }
}
