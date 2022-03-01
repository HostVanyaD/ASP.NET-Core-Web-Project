namespace HighPaw.Web.Controllers
{
    using HighPaw.Data;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Web.Models.Pets;
    using HighPaw.Web.Models.Shelters;
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;
    using System.Linq;

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
                    MicrochipId = p.MicrochipId != null ? p.MicrochipId : "Not info available",
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
    }
}
