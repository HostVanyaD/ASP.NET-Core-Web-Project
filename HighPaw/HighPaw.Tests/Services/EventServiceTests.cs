namespace HighPaw.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Xunit;
    using FluentAssertions;
    using HighPaw.Data.Models;
    using HighPaw.Services.Event;
    using HighPaw.Services.Event.Models;
    using HighPaw.Tests.Mocks;
    using HighPaw.Web.Infrastructure;
    using System.Linq;

    public class EventServiceTests : IDisposable
    {
        private const string testTitle = "Title";
        private const string testDescription = "Description";
        private const string testLocation = "City";     
        private const string testDate = "06/07/2023";
        private const string newTestTitle = "NewTitle";
        private const string newTestDescription = "NewDescription";
        private const string newTestLocation = "NewCity";

        private readonly Data.HighPawDbContext dbContext;
        private readonly IMapper mapper;
        private readonly EventService service;

        public EventServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
            service = new EventService(dbContext, mapper);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void All_ShouldReturnData()
        {
            // Arrange 
            dbContext
                .Events
                .AddRange(new List<Event>
                {
                    new Event
                    {
                        Id = 1,
                        Title = testTitle,
                        Description = testDescription,
                        Location = testLocation,
                        Date = DateTime.UtcNow.AddDays(2)
                    },
                    new Event
                    {
                        Id = 2,
                        Title = testTitle,
                        Description = testDescription,
                        Location = testLocation,
                        Date = DateTime.UtcNow.AddDays(2)
                    }
                }); ;

            dbContext.SaveChanges();

            // Act
            var result = service.All();

            // Assert
            result
                .Should()
                .HaveCount(2)
                .And
                .AllBeOfType<EventServiceModel>();
        }

        [Theory]
        [InlineData(testTitle, testDescription, testLocation, testDate)]
        public void Create_ShouldAddNewEventToDbAndReturnItsId(
            string title,
            string description,
            string location,
            DateTime date)
        {        
            // Act
            service.Create(title, description, location, date);

            dbContext.SaveChanges();

            var result = dbContext
                .Events
                .Count();

            // Assert
            result
                .Should()
                .Be(1);
        }

        [Fact]
        public void Edit_ShouldUpdateAnEvent()
        {
            // Arrange
            var eventId = 1;

            var initialModel = new Event
            {
                Id = eventId,
                Title = testTitle,
                Description = testDescription,
                Location = testLocation,
                Date = DateTime.Parse(testDate)
            };

            dbContext
                .Events
                .Add(initialModel);

            dbContext.SaveChanges();

            var editModel = new EventServiceModel
            {
                Id = eventId,
                Title = newTestTitle,
                Description = newTestDescription,
                Location = newTestLocation,
                Date = testDate
            };

            // Act
            service.Edit(editModel);

            // Assert
            initialModel
                .Title
                .Should()
                .BeEquivalentTo(newTestTitle);

            initialModel
                .Description
                .Should()
                .BeEquivalentTo(newTestDescription);

            initialModel
                .Location
                .Should()
                .BeEquivalentTo(newTestLocation);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectEvent()
        {
            // Arrange
            var eventId = 1;

            var initialModel = new Event
            {
                Id = eventId,
                Title = testTitle,
                Description = testDescription,
                Location = testLocation,
                Date = DateTime.Parse(testDate)
            };

            dbContext
                .Events
                .Add(initialModel);

            dbContext.SaveChanges();

            // Act
            var result = service.GetById(eventId);

            // Assert
            result
                .Id
                .Should()
                .Be(eventId);
        }
        
        [Fact]
        public void DoesExist_ShouldReturnTrueIfEventIsPresent()
        {
            // Arrange
            var eventId = 1;

            dbContext
                .Events
                .Add(new Event
                {
                    Id = eventId,
                    Title = testTitle,
                    Description = testDescription,
                    Location = testLocation,
                    Date = DateTime.Parse(testDate)
                });

            dbContext.SaveChanges();

            // Act
            var result = service.DoesExist(eventId);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public void Delete_ShouldRemoveEventFormDb()
        {
            // Arrange
            var eventId = 1;

            dbContext
                .Events
                .Add(new Event
                {
                    Id = eventId,
                    Title = testTitle,
                    Description = testDescription,
                    Location = testLocation,
                    Date = DateTime.Parse(testDate)
                });

            dbContext.SaveChanges();

            // Act
            service.Delete(eventId);

            // Assert
            dbContext
                .Events
                .Count()
                .Should()
                .Be(0);
        }
    }
}
