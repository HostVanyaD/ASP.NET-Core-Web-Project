namespace HighPaw.Tests.Services
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Xunit;
    using FluentAssertions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Admin;
    using HighPaw.Web.Infrastructure;

    public class AdminServiceTests
    {
        [Fact]
        public void GetLatestPets_ShoudReturnLast10AddedPets()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase("test").Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new AdminService(dbContext, mapper);

            dbContext
                .SizeCategories
                .Add(new SizeCategory { 
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
                    Age = 1+i,
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
                .GetLatestPets();

                // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .HaveCount(10);
        }

        [Fact]
        public void GetLatestArticles_ShoudReturnLast10AddedArticles()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase("test").Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new AdminService(dbContext, mapper);

            var articles = new List<Article>();

            for (int i = 0; i < 12; i++)
            {
                articles.Add(new Article
                {
                    Title = "Title" + i,
                    Content = "Content",
                    ArticleType = ArticleType.Article,
                    ImageUrl = "image",
                    CreatorName = "Test"
                });
            }

            // Act

            dbContext.Articles.AddRange(articles);
            dbContext.SaveChanges();

            // Assert
            service
                .GetLatestArticles()
                .Should()
                .NotBeNull()
                .And
                .HaveCount(10);
        }
    }
}
