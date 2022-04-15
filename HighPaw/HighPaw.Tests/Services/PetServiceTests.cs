namespace HighPaw.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using AutoMapper;
    using HighPaw.Data;
    using HighPaw.Services.Pet;
    using HighPaw.Web.Infrastructure;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using FluentAssertions;
    using HighPaw.Services.Pet.Models;
    using System.Linq;
    using HighPaw.Tests.Mocks;

    public class PetServiceTests : IDisposable
    {
        private readonly Data.HighPawDbContext dbContext;
        private readonly IMapper mapper;
        private readonly PetService service;

        public PetServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
            service = new PetService(dbContext, mapper);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void Latest_ShouldReturnLast3AddedPetsInDescendingOrder()
        {
            // Arrange
            GetDbContextWithMultiplePets();

            // Act
            var result = service
                .Latest();

            // Assert
            result
                .Should()
                .HaveCount(3)
                .And
                .AllBeOfType<PetListingServiceModel>();
        }

        [Fact]
        public void Details_ShouldReturnCorrectModel()
        {
            // Arrange
            var petId = GetPetIdFromDbContextWithASinglePet();

            // Act
            var result = service
                .Details(petId);

            // Assert
            result
                .Should()
                .BeOfType<PetDetailsServiceModel>();
        }

        [Fact]
        public void AllLost_ShouldReturnCollectionWithLostPetsOnly()
        {
            // Arrange
            dbContext
                .SizeCategories
                .Add(new SizeCategory
                {
                    Id = 1,
                    Name = "Size",
                    Description = "Description"
                });

            dbContext.Shelters.Add(new Shelter
            {
                Id = 1,
                Name = "Shelter",
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "+359888888888"
            });

            var pets = new List<Pet>();

            for (int i = 0; i < 5; i++)
            {
                pets.Add(new Pet
                {
                    Name = "pet" + i,
                    ImageUrl = "imageUrl",
                    PetType = PetType.Dog,
                    Breed = "breed",
                    Age = 1 + i,
                    Gender = "Male",
                    Color = "color",
                    IsLost = true,
                    SizeCategoryId = 1,
                    ShelterId = 1
                });
            }

            dbContext.Pets.AddRange(pets);
            dbContext.SaveChanges();

            // Act
            var result = service
                .AllLost();

            var expectedCount = dbContext
                .Pets
                .Select(p =>
                p.IsLost == true)
                .ToList()
                .Count;

            // Assert
            result
                .Should()
                .HaveCount(expectedCount)
                .And
                .AllBeOfType<PetListingServiceModel>();
        }

        [Fact]
        public void AllFound_ShouldReturnCollectionWithFoundPetsOnly()
        {
            // Arrange
            dbContext
                .SizeCategories
                .Add(new SizeCategory
                {
                    Id = 1,
                    Name = "Size",
                    Description = "Description"
                });

            dbContext.Shelters.Add(new Shelter
            {
                Id = 1,
                Name = "Shelter",
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "+359888888888"
            });

            var pets = new List<Pet>();

            for (int i = 0; i < 5; i++)
            {
                pets.Add(new Pet
                {
                    Name = "pet" + i,
                    ImageUrl = "imageUrl",
                    PetType = PetType.Dog,
                    Breed = "breed",
                    Age = 1 + i,
                    Gender = "Male",
                    Color = "color",
                    IsFound = true,
                    SizeCategoryId = 1,
                    ShelterId = 1
                });
            }

            dbContext.Pets.AddRange(pets);
            dbContext.SaveChanges();

            // Act
            var result = service
                .AllFound();

            var expectedCount = dbContext
                .Pets
                .Select(p =>
                p.IsFound == true)
                .ToList()
                .Count;

            // Assert
            result
                .Should()
                .HaveCount(expectedCount)
                .And
                .AllBeOfType<PetListingServiceModel>();
        }

        [Fact]
        public void Adopt_ShouldSetPropertyCorrectly()
        {
            // Arrange
            var petId = GetPetIdFromDbContextWithASinglePet();

            // Act
            service
                .Adopt(petId);

            var result = dbContext
                .Pets
                .Where(p => p.Id == petId)
                .Select(p => p.IsAdopted)
                .FirstOrDefault();

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public void AllCategories_ShouldReturnCollectionWithExistingCategories()
        {
            // Arrange
            dbContext
                .SizeCategories
                .AddRange(new List<SizeCategory>
                {
                    new SizeCategory
                    {
                        Id = 1,
                        Name = "Small",
                        Description = "Description"
                    },
                    new SizeCategory
                    {
                        Id = 2,
                        Name = "Medium",
                        Description = "Description"
                    }
                });

            dbContext.SaveChanges();

            // Act
            var result = service
                .AllCategories();

            var expectedCount = dbContext
                .SizeCategories
                .Count();

            // Assert
            result
                .Should()
                .HaveCount(expectedCount)
                .And
                .AllBeOfType<SizeCategoryServiceModel>();
        }
        
        [Fact]
        public void CategoryExists_ShouldReturnCorrectBool()
        {
            // Arrange
            var categoryId = 1;

            dbContext
                .SizeCategories
                .Add(new SizeCategory
                {
                    Id = categoryId,
                    Name = "Size",
                    Description = "Description"
                });

            dbContext.SaveChanges();

            // Act
            var result = service
                .CategoryExists(categoryId);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData("Pet", "Image", "Dog", "Bulldog", 1, "Male", "Black", null, false, null, null, false, null, null, 1, 1)]
        public void Add_ShouldAddNewPetToTheDb_AndReturnItsId(
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
            // Arrange
            dbContext
                .SizeCategories
                .Add(new SizeCategory
                {
                    Id = 1,
                    Name = "Size",
                    Description = "Description"
                });

            dbContext.Shelters.Add(new Shelter
            {
                Id = 1,
                Name = "Shelter",
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "+359888888888"
            });

            // Act
            var result = service
                .Add(
                    name,
                    imageUrl,
                    petType,
                    breed,
                    age,
                    gender,
                    color,
                    microchipId,
                    isLost,
                    lastSeenLocation,
                    lostDate,
                    isFound,
                    foundLocation,
                    foundDate,
                    sizeCategoryId,
                    shelterId
                );

            var expectedValue = dbContext
                .Pets
                .Select(p => p.Id)
                .FirstOrDefault();

            // Assert
            result
                .Should()
                .Be(expectedValue);
        }

        [Fact]
        public void Edit_ShouldUpdateGivenPet()
        {
            var petId = 1;

            var initialModel = new Pet
            {
                Id = petId,
                Name = "Pet",
                ImageUrl = "ImageUrl",
                PetType = PetType.Dog,
                Breed = "Bulldog",
                Age = 1,
                Gender = "Male",
                Color = "Black",
                MicrochipId = null,
                IsLost = true,
                LastSeenLocation = "Location",
                LostDate = null,
                IsFound = false,
                FoundLocation = null,
                FoundDate = null,
                SizeCategoryId = 1,
                ShelterId = 2
            };

            dbContext
                .Pets
                .Add(initialModel);

            dbContext.SaveChanges();

            var editModel = new EditPetServiceModel
            {
                Id = petId,
                Name = "Pet",
                ImageUrl = "ImageUrl",
                PetType = "Dog",
                Breed = "Bulldog",
                Age = 1,
                Gender = "Female",
                Color = "Black",
                MicrochipId = null,
                IsLost = true,
                LastSeenLocation = "Location",
                LostDate = null,
                IsFound = false,
                FoundLocation = null,
                FoundDate = null,
                SizeCategoryId = 1,
                ShelterId = 2
            };

            // Act
            service.Edit(editModel);

            // Assert
            initialModel
                .Gender
                .Should()
                .BeEquivalentTo(editModel.Gender);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectPetIsPresent()
        {
            // Arrange
            var petId = GetPetIdFromDbContextWithASinglePet();

            // Act
            var result = service.GetById(petId);

            // Assert
            result
                .Id
                .Should()
                .Be(petId);
        }
        
        [Fact]
        public void DoesExist_ShouldReturnTrueIfPetIsPresent()
        {
            // Arrange
            var petId = GetPetIdFromDbContextWithASinglePet();

            // Act
            var result = service.DoesExist(petId);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public void All_ShouldReturnCollectionOfListingPetModel()
        {
            // Arrange
            GetDbContextWithMultiplePets();

            // Act
            var result = service
                .All();

            var expectedCount = dbContext
                .Pets
                .Count();

            // Assert
            result
                .Should()
                .HaveCount(expectedCount)
                .And
                .AllBeOfType<PetListingServiceModel>();
        }

        [Theory]
        [InlineData(1, 9, null)]
        [InlineData(1, 3, "1")]
        public void AllOverload_ShouldReturnCorrectType(
            int currentPage,
            int pageSize,
            string searchString)
        {
            // Arrange
             GetDbContextWithMultiplePets();

            // Act
            var result = service
                .All(currentPage, pageSize, searchString);

            // Assert
            result
                .Should()
                .BeOfType<PetQueryServiceModel>();
        }

        [Theory]
        [InlineData(1, 3, "b,a,c")]
        public void GetQuizResults_ShouldReturnCorrectType(
            int currentPage,
            int pageSize,
            string filters)
        {
            // Arrange
            GetDbContextWithMultiplePets();

            // Act
            var result = service
                .GetQuizResults(currentPage, pageSize, filters);

            // Assert
            result
                .Should()
                .BeOfType<PetQueryServiceModel>();
        }

        [Fact]
        public void Delete_ShouldRemovePetWithGivenIdFromDb_AndReturnTrueIfSucceeded()
        {
            // Arrange
            var petId = GetPetIdFromDbContextWithASinglePet();

            // Act
            var result = service
                .Delete(petId);

            // Assert
            result
                .Should()
                .BeTrue();

            dbContext
                .Pets
                .Count()
                .Should()
                .Be(0);
        }

        [Fact]
        public void Delete_ShouldReturnFalseIfPetIdIsNotPresent()
        {
            // Arrange
            var petId = GetPetIdFromDbContextWithASinglePet();

            var invalidId = petId + 1;

            // Act
            var result = service
                .Delete(invalidId);

            // Assert
            result
                .Should()
                .BeFalse();
        }

        private void GetDbContextWithMultiplePets()
        {
            dbContext
                .SizeCategories
                .Add(new SizeCategory
                {
                    Id = 1,
                    Name = "Size",
                    Description = "Description"
                });

            dbContext.Shelters.Add(new Shelter
            {
                Id = 1,
                Name = "Shelter",
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "+359888888888"
            });

            var pets = new List<Pet>();

            for (int i = 0; i < 12; i++)
            {
                pets.Add(new Pet
                {
                    Name = "pet" + i,
                    ImageUrl = "imageUrl",
                    PetType = PetType.Dog,
                    Breed = "breed",
                    Age = 1 + i,
                    Gender = "Male",
                    Color = "color",
                    SizeCategoryId = 1,
                    ShelterId = 1
                });
            }

            dbContext.Pets.AddRange(pets);
            dbContext.SaveChanges();
        }
        
        private int GetPetIdFromDbContextWithASinglePet() 
        { 
            dbContext
                .SizeCategories
                .Add(new SizeCategory
                {
                    Id = 1,
                    Name = "Size",
                    Description = "Description"
                });

            dbContext.Shelters.Add(new Shelter
            {
                Id = 1,
                Name = "Shelter",
                Address = "Address",
                Email = "email@email.com",
                PhoneNumber = "+359888888888"
            });

            var petId = 1;

            dbContext
                .Pets
                .Add(new Pet
                {
                    Id = petId,
                    Name = "pet",
                    ImageUrl = "imageUrl",
                    PetType = PetType.Dog,
                    Breed = "breed",
                    Age = 1,
                    Gender = "Male",
                    Color = "color",
                    SizeCategoryId = 1,
                    ShelterId = 1
                });

            dbContext.SaveChanges();

            return petId;
        }
    }
}
