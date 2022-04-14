namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Pet;
    using HighPaw.Services.Pet.Models;
    using HighPaw.Services.Shelter;
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : BaseController
    {
        private readonly IPetService pets;
        private readonly IShelterService shelters;

        public PetsController(
            IPetService pets,
            IShelterService shelters)
        {
            this.pets = pets;
            this.shelters = shelters;
        }

        public IActionResult All()
        {
            var allPets = this.pets
                .All();

            return View(allPets);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var model = this.pets
                .GetById(id);

            if (model is null)
            {
                return NotFound();
            }

            model.Categories = this.pets.AllCategories();
            model.Shelters = this.shelters.GetAllNames();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditPetServiceModel model)
        {
            if (!this.pets.DoesExist(model.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.pets.Edit(model);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            var isDeleteSuccessfull = this.pets.Delete(id);

            return View(isDeleteSuccessfull);
        }
    }
}
