namespace HighPaw.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using HighPaw.Services.Pet;
    using HighPaw.Services.Shelter;
    using HighPaw.Web.Models.Pets;
    using HighPaw.Services.Pet.Models;

    public class PetsController : Controller
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var pet = this.pets.Details(id);

            if (pet == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(pet);
        }
        public IActionResult All([FromQuery] AllPetsQueryModel query)
        {
            var queryResult = this.pets.All(
                query.CurrentPage,
                query.PageSize,
                query.SearchString);

            query.TotalItems = queryResult.TotalItems;
            query.Pets = queryResult.Items;

            return View(query);
        }

        public IActionResult AllLost()
        {
            var lostPets = this.pets.AllLost();

            return View(lostPets);
        }

        public IActionResult AllFound()
        {
            var foundPets = this.pets.AllFound();

            return View(foundPets);
        }

        [Authorize]
        public IActionResult AdoptMe(int id)
        {
            this.pets.Adopt(id);

            var pet = this.pets.Details(id);

            return View(pet);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View(new AddPetFormModel
            {
                Categories = this.pets.AllCategories(),
                Shelters = this.shelters.GetAllNames()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddPetFormModel pet)
        {
            if (!this.pets.CategoryExists(pet.SizeCategoryId))
            {
                this.ModelState.AddModelError(nameof(pet.SizeCategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                pet.Categories = this.pets.AllCategories();
                pet.Shelters = this.shelters.GetAllNames();

                return View(pet);
            }

            var petId = this.pets
                .Add(
                pet.Name,
                pet.ImageUrl,
                pet.PetType,
                pet.Breed,
                pet.Age,
                pet.Gender,
                pet.Color,
                pet.MicrochipId,
                pet.IsLost,
                pet.LastSeenLocation,
                pet.LostDate,
                pet.IsFound,
                pet.FoundLocation,
                pet.FoundDate,
                pet.SizeCategoryId,
                pet.ShelterId
                );

            return RedirectToAction(nameof(this.AllFound));
        }
    }
}
