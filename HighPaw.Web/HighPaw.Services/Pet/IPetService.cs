namespace HighPaw.Services.Pet
{
    using HighPaw.Services.Pet.Models;
    using System;
    using System.Collections.Generic;

    public interface IPetService
    {
        public IEnumerable<PetListingServiceModel> Latest();

        public PetDetailsServiceModel Details(int id);

        public IEnumerable<PetListingServiceModel> AllLost();

        public IEnumerable<PetListingServiceModel> AllFound();

        public void Adopt(int id);

        public IEnumerable<SizeCategoryServiceModel> AllCategories();

        public bool CategoryExists(int sizeCategoryId);

        public int Add(
            string Name,
            string ImageUrl,
            string PetType,
            string Breed,
            int? Age,
            string Gender,
            string Color,
            string MicrochipId,
            bool IsLost,
            string LastSeenLocation,
            DateTime? LostDate,
            bool IsFound,
            string FoundLocation,
            DateTime? FoundDate,
            int SizeCategoryId,
            int ShelterId);

        public IEnumerable<PetListingServiceModel> All();

        public bool Delete(int id);
    }
}
