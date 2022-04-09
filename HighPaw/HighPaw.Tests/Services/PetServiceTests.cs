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

    public class PetServiceTests
    {
        [Fact]
        public void Latest_ShouldReturnLast3AddedPetsInDescendingOrder()
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

            // Act
            var result = service
                .Details(petId);

            // Assert
            result
                .Should()
                .BeOfType<PetDetailsServiceModel>();
        }
    }
}
