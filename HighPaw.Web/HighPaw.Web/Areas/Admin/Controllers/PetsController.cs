namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Pet;
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : BaseController
    {
        private readonly IPetService pets;

        public PetsController(IPetService pets)
            => this.pets = pets;

        public IActionResult All()
        {
            var allPets = this.pets
                .All();

            return View(allPets);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Edit()
        //{
        //    return View();
        //}

        public IActionResult Delete(int id)
        {
            var isDeleteSuccessfull = this.pets.Delete(id);

            return View(isDeleteSuccessfull);
        }
    }
}
