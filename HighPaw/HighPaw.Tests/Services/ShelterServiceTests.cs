namespace HighPaw.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Xunit;
    using FluentAssertions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Services.Shelter;
    using HighPaw.Web.Infrastructure;
    using HighPaw.Services.Shelter.Models;

    public class ShelterServiceTests
    {
        [Fact]
        public void GetAllNames_ShouldReturnCollectionOfCorrectType()
        {
            // Arrange
            var (service, dbContext) = GetServiceAndDbContextWithMultipleShelters();

            // Act
            var result = service
                .GetAllNames();

            var expectedCount = dbContext
                .Shelters
                .Count();

            // Assert
            result
                .Should()
                .HaveCount(expectedCount)
                .And
                .AllBeOfType<ShelterNameServiceModel>();

            dbContext.Dispose();
        }

        [Fact]
        public void All_ShouldReturnCollectionOfCorrectType()
        {
            // Arrange
            var (service, dbContext) = GetServiceAndDbContextWithMultipleShelters();

            // Act
            var result = service
                .All();

            var expectedCount = dbContext
               .Shelters
               .Count();

            // Assert
            result
                .Should()
                .HaveCount(expectedCount)
                .And
                .AllBeOfType<ShelterServiceModel>();

            dbContext.Dispose();
        }

        [Fact]
        public void Delete_ShouldRemoveShelterWithGivenIdFromDb()
        {
            // Arrange
            var (service, dbContext, shelterId) = GetServiceDbContextWithASingleShelterAndItsId();

            // Act
            service
                .Delete(shelterId);

            // Assert
            dbContext
                .Shelters
                .Count()
                .Should()
                .Be(0);

            dbContext.Dispose();
        }

        [Theory]
        [InlineData("Shelter", "City", "shelter@shelter.com", "+359888888888", "Description", "sheter.com")]
        public void Add_ShouldAddNewShelterToTheDb_AndReturnItsId(
            string name,
            string address,
            string email,
            string phoneNumber,
            string description,
            string website)
        {
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ShelterService(dbContext, mapper);

            // Act
            var result = service
                .Add(name, address, email, phoneNumber, description, website);
           
            var expectedValue = dbContext
                .Shelters
                .Select(p => p.Id)
                .FirstOrDefault();

            // Assert
            result
                .Should()
                .Be(expectedValue);

            dbContext.Dispose();
        }

        private static (ShelterService, HighPawDbContext) GetServiceAndDbContextWithMultipleShelters()
        {
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ShelterService(dbContext, mapper);

            dbContext
                .Shelters
                .AddRange(new List<Shelter> {
                    new Shelter
                    {
                        Id = 1,
                        Name = "Shelter",
                        Address = "Address",
                        Email = "email@email.com",
                        PhoneNumber = "+359888888888"
                    },
                    new Shelter
                    {
                        Id = 2,
                        Name = "Shelter2",
                        Address = "Address",
                        Email = "email@email.com",
                        PhoneNumber = "+359888888888"
                    }

                });

            dbContext.SaveChanges();

            return (service, dbContext);
        }

        private static (ShelterService, HighPawDbContext, int) GetServiceDbContextWithASingleShelterAndItsId()
        {
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ShelterService(dbContext, mapper);

            var shelterId = 1;

            dbContext
                .Shelters
                .Add(new Shelter
                 {
                     Id = shelterId,
                     Name = "Shelter",
                     Address = "Address",
                     Email = "email@email.com",
                     PhoneNumber = "+359888888888"
                 });

            dbContext.SaveChanges();

            return (service, dbContext, shelterId);
        }
    }
}
