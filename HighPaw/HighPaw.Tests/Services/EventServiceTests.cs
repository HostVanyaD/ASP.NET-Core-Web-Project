namespace HighPaw.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using FluentAssertions;
    using AutoMapper;
    using Xunit;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Services.Event;
    using HighPaw.Services.Event.Models;
    using HighPaw.Web.Infrastructure;

    public class EventServiceTests
    {
        [Fact]
        public void All_ShouldReturnData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new EventService(dbContext, mapper);

            dbContext
                .Events
                .AddRange(new List<Event>
                {
                    new Event
                    {
                        Id = 1,
                        Title = "Title",
                        Description = "Description",
                        Location = "City"
                    },
                    new Event
                    {
                        Id = 2,
                        Title = "Title",
                        Description = "Description",
                        Location = "City"
                    }
                });

            dbContext.SaveChanges();

            // Act
            var result = service.All();

            // Assert
            result
                .Should()
                .HaveCount(2)
                .And
                .AllBeOfType<EventServiceModel>();

            dbContext.Dispose();
        }
    }
}
