namespace HighPaw.Services.Pet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Pet.Models;

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

        public IEnumerable<PetListingServiceModel> All()
            => this.data
                .Pets
                .ProjectTo<PetListingServiceModel>(this.mapper)
                .ToList();

        public PetQueryServiceModel All(
            int currentPage = 1,
            int pageSize = 9,
            string searchString = null)
        {
            IQueryable<Pet> petsQuery;

            if (searchString == null)
            {
                petsQuery = this.data.Pets;
            }
            else
            {
                // TODO: I have to improve searching, because when you search for "male", results include "female" as well
                petsQuery = this.data
                     .Pets
                     .Where(p =>
                        (p.Breed + " " + p.Age + " " + p.Gender).ToLower()
                        .Contains(searchString.Trim().ToLower()));
            }

            var totalPets = petsQuery.Count();

            var petsList = GetPets(petsQuery
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize));


            return new PetQueryServiceModel
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalPets,
                Items = petsList
            };
        }

        public bool Delete(int id)
        {
            var petToDelete = this.data
                .Pets
                .Find(id);

            if (petToDelete is null)
            {
                return false;
            }

            this.data.Remove(petToDelete);
            this.data.SaveChanges();

            return true;
        }

        public PetQueryServiceModel GetQuizResults(
            int currentPage,
            int pageSize,
            string filters)
        {
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 6;
            }

            IQueryable<Pet> petsQuery = this.data.Pets;
            petsQuery = GenerateResults(filters, petsQuery);

            var totalPets = petsQuery.Count();

            var petsList = GetPets(petsQuery
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize));


            return new PetQueryServiceModel
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalPets,
                Items = petsList
            };
        }

        private static IQueryable<Pet> GenerateResults(string filters, IQueryable<Pet> petsQuery)
        {
            var answers = filters.Split(',');

            for (int i = 0; i < answers.Length; i++)
            {
                var answer = answers[i];

                if (i == 0)
                {
                    petsQuery = answer switch
                    {
                        "a" => petsQuery,
                        "b" => petsQuery.Where(p => p.SizeCategoryId == 1),
                        "c" => petsQuery.Where(p => p.SizeCategoryId == 2),
                        "d" => petsQuery.Where(p => p.SizeCategoryId == 3),
                        "e" => petsQuery.Where(p => p.SizeCategoryId == 4),
                        _ => petsQuery
                    };
                }
                else if (i == 1)
                {
                    petsQuery = answer switch
                    {
                        "a" => petsQuery,
                        "b" => petsQuery.Where(p => p.Age <= 1),
                        "c" => petsQuery.Where(p => p.Age > 1 && p.Age < 2),
                        "d" => petsQuery.Where(p => p.Age >= 2 && p.Age < 6),
                        "e" => petsQuery.Where(p => p.Age > 6),
                        _ => petsQuery
                    };
                }
                else
                {
                    petsQuery = answer switch
                    {
                        "a" => petsQuery,
                        "b" => petsQuery.Where(p => p.Gender.ToLower() == "male"),
                        "c" => petsQuery.Where(p => p.Gender.ToLower() == "female"),
                        _ => petsQuery
                    };
                }
            }

            return petsQuery;
        }

        private IEnumerable<PetListingServiceModel> GetPets(IQueryable<Pet> petsQuery)
            => petsQuery
                .ProjectTo<PetListingServiceModel>(this.mapper)
                .ToList();
    }
}
