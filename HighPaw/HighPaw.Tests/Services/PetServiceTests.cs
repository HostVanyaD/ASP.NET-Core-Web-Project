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

    public class PetServiceTests
    {
        [Fact]
        public void Latest_ShouldReturnLast3AddedPetsInDescendingOrder()
        {
            // Arrange
            var (service, dbContext) = GetServiceAndDbContextWithMultiplePets();

            // Act
            var result = service
                .Latest();

            // Assert
            result
                .Should()
                .HaveCount(3)
                .And
                .AllBeOfType<PetListingServiceModel>();

            dbContext.Dispose();
        }

        [Fact]
        public void Details_ShouldReturnCorrectModel()
        {
            // Arrange
            var (service, dbContext, petId) = GetServiceDbContextWithASinglePetAndItsId();

            // Act
            var result = service
                .Details(petId);

            // Assert
            result
                .Should()
                .BeOfType<PetDetailsServiceModel>();

            dbContext.Dispose();
        }

        [Fact]
        public void AllLost_ShouldReturnCollectionWithLostPetsOnly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            dbContext.Dispose();
        }

        [Fact]
        public void AllFound_ShouldReturnCollectionWithFoundPetsOnly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            dbContext.Dispose();
        }

        [Fact]
        public void Adopt_ShouldSetPropertyCorrectly()
        {
            // Arrange
            var (service, dbContext, petId) = GetServiceDbContextWithASinglePetAndItsId();

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

            dbContext.Dispose();
        }

        [Fact]
        public void AllCategories_ShouldReturnCollectionWithExistingCategories()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            dbContext.Dispose();
        }
        
        [Fact]
        public void CategoryExists_ShouldReturnCorrectBool()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            dbContext.Dispose();
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
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            dbContext.Dispose();
        }

        [Fact]
        public void All_ShouldReturnCollectionOfListingPetModel()
        {
            // Arrange
            var (service, dbContext) = GetServiceAndDbContextWithMultiplePets();

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

            dbContext.Dispose();
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
            var (service, dbContext) = GetServiceAndDbContextWithMultiplePets();

            // Act
            var result = service
                .All(currentPage, pageSize, searchString);

            // Assert
            result
                .Should()
                .BeOfType<PetQueryServiceModel>();

            dbContext.Dispose();
        }

        [Theory]
        [InlineData(1, 3, "b,a,c")]
        public void GetQuizResults_ShouldReturnCorrectType(
            int currentPage,
            int pageSize,
            string filters)
        {
            // Arrange
            var (service, dbContext) = GetServiceAndDbContextWithMultiplePets();

            // Act
            var result = service
                .GetQuizResults(currentPage, pageSize, filters);

            // Assert
            result
                .Should()
                .BeOfType<PetQueryServiceModel>();

            dbContext.Dispose();
        }

        [Fact]
        public void Delete_ShouldRemovePetWithGivenIdFromDb_AndReturnTrueIfSucceeded()
        {
            // Arrange
            var (service, dbContext, petId) = GetServiceDbContextWithASinglePetAndItsId();

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

            dbContext.Dispose();
        }

        [Fact]
        public void Delete_ShouldReturnFalseIfPetIdIsNotPresent()
        {
            // Arrange
            var (service, dbContext, petId) = GetServiceDbContextWithASinglePetAndItsId();

            var invalidId = petId + 1;

            // Act
            var result = service
                .Delete(invalidId);

            // Assert
            result
                .Should()
                .BeFalse();

            dbContext.Dispose();
        }

        private static (PetService, HighPawDbContext) GetServiceAndDbContextWithMultiplePets()
        {
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            return (service, dbContext);
        }
        
        private static (PetService, HighPawDbContext, int) GetServiceDbContextWithASinglePetAndItsId() 
        { 
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new PetService(dbContext, mapper);

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

            return (service, dbContext, petId);
        }
    }
}
