namespace HighPaw.Services.Pet
{

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Pet.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PetService : IPetService
    {
        private readonly HighPawDbContext data;
        private readonly IConfigurationProvider mapper;

        public PetService(HighPawDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<PetListingServiceModel> Latest()
            => this.data
                .Pets
                .OrderByDescending(c => c.Id)
                .ProjectTo<PetListingServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public PetDetailsServiceModel Details(int id)
            => this.data
                .Pets
                .Where(p => p.Id == id)
                .ProjectTo<PetDetailsServiceModel>(this.mapper)
                .FirstOrDefault();


        public IEnumerable<PetListingServiceModel> AllLost()
            => this.data
                .Pets
                .Where(p => p.IsLost == true)
                .ProjectTo<PetListingServiceModel>(this.mapper)
                .ToList();

        public IEnumerable<PetListingServiceModel> AllFound()
        => this.data
                .Pets
                .Where(p => p.IsFound == true)
                .ProjectTo<PetListingServiceModel>(this.mapper)
                .ToList();

        public void Adopt(int id)
        {
            var petToAdopt = this.data
                .Pets
                .Find(id);

            petToAdopt.IsAdopted = true;

            this.data.Update(petToAdopt);
            this.data.SaveChanges();
        }

        public IEnumerable<SizeCategoryServiceModel> AllCategories()
            => this.data
                .SizeCategories
                .ProjectTo<SizeCategoryServiceModel>(this.mapper)
                .ToList();

        public bool CategoryExists(int sizeCategoryId)
            => this.data
            .SizeCategories
            .Any(c => c.Id == sizeCategoryId);

        public int Add(
            string name, 
            string imageUrl, 
            string petType, 
            string breed, 
            int? age, 
            string gender, 
            string color, 
            string microchipId, 
            bool isLost, 
            string lastSeenLocation, 
            DateTime? lostDate, 
            bool isFound, 
            string foundLocation, 
            DateTime? foundDate, 
            int sizeCategoryId, 
            int shelterId)
        {
            var petData = new Pet
            {
                Name = name,
                ImageUrl = imageUrl,
                PetType = Enum.Parse<PetType>(petType),
                Breed = breed,
                Age = age,
                Gender = gender,
                Color = color,
                MicrochipId = microchipId,
                IsLost = isLost,
                LastSeenLocation = lastSeenLocation,
                LostDate = lostDate,
                IsFound = isFound,
                FoundLocation = foundLocation,
                FoundDate = foundDate,
                SizeCategoryId = sizeCategoryId,
                ShelterId = shelterId
            };

            this.data.Pets.Add(petData);
            this.data.SaveChanges();

            return petData.Id;
        }

        // Admin services
        public IEnumerable<PetListingServiceModel> All()
        => this.data
                .Pets
                .ProjectTo<PetListingServiceModel>(this.mapper)
                .ToList();

        public bool Delete(int id)
        {
            var petToDelete = this.data
                .Pets
                .Find(id);

            if(petToDelete is null)
            {
                return false;
            }

            this.data.Remove(petToDelete);
            this.data.SaveChanges();

            return true;
        }
    }
}
