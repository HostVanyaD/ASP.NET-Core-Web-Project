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
    using HighPaw.Services.Volunteer;
    using HighPaw.Data.Models;
    using System.Security.Claims;

    public class VlunteerServiceTests
    {
        [Fact]
        public void IsVolunteer_ShouldReturnTrueIfGivenIdExists()
        {
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var service = new VolunteerService(dbContext);

            dbContext
                .Users
                .Add(new User
                {
                    FullName = "Full Name"
                });

            //var user = new Appl

           // dbContext
                //.Volunteers
                //.Add(new Volunteer
                //{
                //    Id = 1,
                //    FirstName = "First",
                //    LastName = "Last",
                //    Email = "test@volunteer.com",
                //    AllAboutYou = "Some text",
                //    UserId = 1
                //});
        }
    }
}
