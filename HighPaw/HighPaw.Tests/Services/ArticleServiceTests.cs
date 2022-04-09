namespace HighPaw.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Xunit;
    using AutoMapper;
    using FluentAssertions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Article;
    using HighPaw.Services.Article.Models;
    using HighPaw.Web.Infrastructure;

    public class ArticleServiceTests
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

            var service = new ArticleService(dbContext, mapper);

            dbContext
                .Articles
                .AddRange(new List<Article>
                {
                    new Article
                    {
                        Title = "Title",
                        Content = "Content",
                        ArticleType = ArticleType.Article,
                        ImageUrl = "image",
                        CreatorName = "Test"
                    },
                    new Article
                    {
                        Title = "Title",
                        Content = "Content",
                        ArticleType = ArticleType.Article,
                        ImageUrl = "image",
                        CreatorName = "Test"
                    }
                });

            dbContext.SaveChanges();

            // Assert
            service
                .All()
                .Should()
                .NotBeEmpty()
                .And
                .HaveCount(2)
                .And
                .AllBeOfType<ArticleServiceModel>();
        }

        [Theory]
        [InlineData("Title", "Content", "Image", "CreatorName", "Article")]
        public void Create_ShouldCreateNewArticleAndAddItToDb_AndReturnItsId(
            string title, string content, string imageUrl, string creatorName, string articleType)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ArticleService(dbContext, mapper);

            // Act
            var result = service
                .Create(title,
                        content,
                        imageUrl,
                        creatorName,
                        articleType
                        );

            // Assert
            result
                .Should()
                .BeOfType(typeof(int));

            service
                .All()
                .Should()
                .HaveCount(1)
                .And
                .AllBeOfType<ArticleServiceModel>();
        }

        [Theory]
        [InlineData(null, null, "I", null, "Article")]
        public void Create_ShouldThorwDbUpdateException_WhenModelStateIsNotValid(
            string title, string content, string imageUrl, string creatorName, string articleType)
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ArticleService(dbContext, mapper);

            // Act
            Action act = () => service
                .Create(title,
                        content,
                        imageUrl,
                        creatorName,
                        articleType
                        );

            // Assert
            act
                .Should()
                .Throw<Microsoft.EntityFrameworkCore.DbUpdateException>();
        }

        [Fact]
        public void Delete_ShouldRemoveTheArticleFromDb()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ArticleService(dbContext, mapper);

            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = "Title",
                    Content = "Content",
                    ArticleType = ArticleType.Article,
                    ImageUrl = "image",
                    CreatorName = "Test"
                });

            dbContext.SaveChanges();

            // Act
            service.Delete(articleId);

            // Assert
            dbContext
                .Articles
                .Should()
                .HaveCount(0);
        }

        [Fact]
        public void Read_ShouldReturnAnArticleOfCorrectType()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HighPawDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new HighPawDbContext(options);

            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new ArticleService(dbContext, mapper);

            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = "Title",
                    Content = "Content",
                    ArticleType = ArticleType.Article,
                    ImageUrl = "image",
                    CreatorName = "Test"
                });

            dbContext.SaveChanges();

            // Act
            var result = service
                .Read(articleId);

                // Assert
            result
                .Should()
                .BeOfType<ArticleServiceModel>();
        }
    }
}
