namespace HighPaw.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using HighPaw.Data;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Web.Models.Pets;
    using HighPaw.Web.Models.Shelters;
    using Microsoft.AspNetCore.Authorization;

    public class PetsController : Controller
    {
        private readonly HighPawDbContext data;

        public PetsController(HighPawDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var pet = this.data
                .Pets
                .Where(p => p.Id == id)
                .Select(p => new PetDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age,
                    PetType = p.PetType == PetType.Dog ? "Dog" : "Cat",
                    Breed = p.Breed,
                    Gender = p.Gender,
                    Color = p.Color,
                    MicrochipId = p.MicrochipId ?? "Not info available",
                    IsAdopted = p.IsAdopted,
                    IsFound = p.IsFound,
                    IsLost = p.IsLost,
                    FoundDate = p.FoundDate.ToString(),
                    LostDate = p.LostDate.ToString(),
                    FoundLocation = p.FoundLocation,
                    LastSeenLocation = p.LastSeenLocation,
                    SizeCategory = p.SizeCategory.Name,
                    Shelter = new ShelterDetailsViewModel
                    {
                        Id = p.Shelter.Id,
                        Name = p.Shelter.Name,
                        Address = p.Shelter.Address,
                        PhoneNumber = p.Shelter.PhoneNumber,
                        Website = p.Shelter.Website
                    }
                        
                })
                .FirstOrDefault();

            if (pet == null)
            {
                return RedirectToAction("Home", "Error");
            }

            return View(pet);
        }

        public IActionResult AllLost()
        {
            var lostPets = this.data
                .Pets
                .Where(p => p.IsLost == true)
                .Select(p => new PetListingViewModel
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Age = p.Age,
                    Gender = p.Gender,
                    ShelterName = p.Shelter.Name
                })
                .ToList();

            return View(lostPets);
        }

        [Authorize]
        public IActionResult AdoptMe(int id)
        {
            var petToAdopt = this.data
                .Pets
                .Find(id);

            petToAdopt.IsAdopted = true;

            this.data.Update(petToAdopt);
            this.data.SaveChanges();

            var pet = this.data
                .Pets
                .Where(p => p.Id == id)
                .Select(p => new AdoptedPetViewModel
                {
                    Name = p.Name,
                    Age = p.Age,
                    Gender = p.Gender,
                    ImageUrl = p.ImageUrl,
                    Shelter = p.Shelter.Name
                })
                .FirstOrDefault();

            return View(pet);
        }

        [Authorize]
        public IActionResult AddPet()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPet(AddPetFormModel pet)
        {
            return View();
        }
    }
}
