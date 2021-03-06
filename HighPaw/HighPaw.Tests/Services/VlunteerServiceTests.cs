namespace HighPaw.Tests.Services
{
    using System;
    using System.Linq;
    using Xunit;
    using FluentAssertions;
    using HighPaw.Data.Models;
    using HighPaw.Services.Volunteer;
    using HighPaw.Tests.Mocks;

    public class VlunteerServiceTests
    {
        [Fact]
        public void IsVolunteer_ShouldReturnTrueIfGivenIdExists()
        {
            // Arrange
            var dbContext = DatabaseMock.Instance;
            var userManagerMock = UserManagerMock.Instance;
            var signInManagerMock = SignInManagerMock.Instance;

            var service = new VolunteerService(dbContext, userManagerMock, signInManagerMock);

            var user = new User { Id = Guid.NewGuid().ToString(), FullName = "Name" };

            dbContext
                .Users
                .Add(user);

            dbContext
                .Volunteers
                .Add(new Volunteer
                {
                    Id = 1,
                    FirstName = "First",
                    LastName = "Last",
                    Email = "test@volunteer.com",
                    AllAboutYou = "Some text",
                    UserId = user.Id
                });

            dbContext.SaveChanges();

            // Act
            var result = service
                .IsVolunteer(user.Id);

            // Assert
            result
                .Should()
                .BeTrue();

            dbContext.Dispose();
        }

        [Theory]
        [InlineData("First", "Last", "test@email.com", "Description", "testId")]
        public void Become_ShouldMakeAnUserAVolunteer_AndReturnHisId(
            string firstName,
            string lastName,
            string email,
            string allAboutYou,
            string userId)
        {
            // Arrange
            var dbContext = DatabaseMock.Instance;
            var userManagerMock = UserManagerMock.Instance;
            var signInManagerMock = SignInManagerMock.Instance;

            var service = new VolunteerService(dbContext, userManagerMock, signInManagerMock);

            var user = new User { Id = userId, FullName = "Name" };

            dbContext
                .Users
                .Add(user);

            dbContext.SaveChanges();

            // Act
            var result = service
                .Become(firstName, lastName, email, allAboutYou, userId);

            var expectedId = dbContext
                .Volunteers
                .Select(v => v.Id)
                .FirstOrDefault();

            // Assert
            result
                .Should()
                .Be(expectedId);

            dbContext
                .Volunteers
                .Count()
                .Should()
                .Be(1);

            dbContext.Dispose();
        }
    }
}
