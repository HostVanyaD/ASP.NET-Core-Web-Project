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
    using HighPaw.Tests.Mocks;

    public class ShelterServiceTests : IDisposable
    {
        private const string testName = "TestName";
        private const string testAddress = "TestAddress";
        private const string testEmail = "test@email.com";
        private const string testPhoneNumber = "+359888888888";
        private const string testDescription = "Description";
        private const string testWebsite = "sheter.com";


        private readonly Data.HighPawDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ShelterService service;

        public ShelterServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
            service = new ShelterService(dbContext, mapper);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void GetAllNames_ShouldReturnCollectionOfCorrectType()
        {
            // Arrange
            GetDbContextWithMultipleShelters();

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
        }

        [Fact]
        public void All_ShouldReturnCollectionOfCorrectType()
        {
            // Arrange
            GetDbContextWithMultipleShelters();

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
        }

        [Fact]
        public void Delete_ShouldRemoveShelterWithGivenIdFromDb()
        {
            // Arrange
            var shelterId = GetShelterIdFromDbContextWithASingleShelter();

            // Act
            service
                .Delete(shelterId);

            // Assert
            dbContext
                .Shelters
                .Count()
                .Should()
                .Be(0);
        }

        [Theory]
        [InlineData(testName, testAddress, testEmail, testPhoneNumber, testDescription, testWebsite)]
        public void Add_ShouldAddNewShelterToTheDb_AndReturnItsId(
            string name,
            string address,
            string email,
            string phoneNumber,
            string description,
            string website)
        {
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
        }

        [Fact]
        public void Edit_ShouldUpdateGivenShelter()
        {
            // Arrange
            var shelterId = 1;

            var initialModel = new Shelter
            {
                Id = shelterId,
                Name = testName,
                Address = testAddress,
                Email = testEmail,
                Description = testDescription,
                PhoneNumber = testPhoneNumber,
                Website = testWebsite
            };

            dbContext
                .Shelters
                .Add(initialModel);

            dbContext.SaveChanges();

            var editModel = new ShelterServiceModel
            {
                Id = shelterId,
                Name = testName + "New",
                Address = testAddress + "New",
                Email = testEmail,
                Description = testDescription,
                PhoneNumber = testPhoneNumber,
                Website = testWebsite
            };

            // Act
            service.Edit(editModel);

            // Arrange
            initialModel
                .Name
                .Should()
                .BeEquivalentTo(editModel.Name);

            initialModel
                .Address
                .Should()
                .BeEquivalentTo(editModel.Address);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectShelterIsPresent()
        {
            // Arrange
            var shelterId = GetShelterIdFromDbContextWithASingleShelter();

            // Act
            var result = service.GetById(shelterId);

            // Assert
            result
                .Id
                .Should()
                .Be(shelterId);
        }

        [Fact]
        public void DoesExist_ShouldReturnTrueIfShelterIsPresent()
        {
            // Arrange
            var shelterId = GetShelterIdFromDbContextWithASingleShelter();

            // Act
            var result = service.DoesExist(shelterId);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        private void GetDbContextWithMultipleShelters()
        {
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
        }

        private int GetShelterIdFromDbContextWithASingleShelter()
        {
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

            return shelterId;
        }
    }
}
