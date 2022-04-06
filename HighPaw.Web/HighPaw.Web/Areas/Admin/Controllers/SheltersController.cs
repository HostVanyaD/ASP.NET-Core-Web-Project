namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Shelter;
    using HighPaw.Services.Shelter.Models;
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

        public IActionResult Add()
            => View();

        [HttpPost]
        public IActionResult Add(AddShelterFormServiceModel shelter)
        {
            if (!ModelState.IsValid)
            {
                return View(shelter);
            }

            var shelterId = this.shelters
                .Add(
                shelter.Name,
                shelter.Address,
                shelter.Email,
                shelter.PhoneNumber,
                shelter.Description,
                shelter.Website);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            this.shelters.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
