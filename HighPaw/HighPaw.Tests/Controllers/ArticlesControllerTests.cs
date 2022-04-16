namespace HighPaw.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;
    using AutoMapper;
    using FluentAssertions;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Article;
    using HighPaw.Tests.Mocks;
    using HighPaw.Web.Controllers;
    using HighPaw.Web.Infrastructure;
    using HighPaw.Web.Models.Articles;

    public class ArticlesControllerTests : IDisposable
    {
        private readonly Data.HighPawDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ArticleService service;
        private readonly ArticlesController controller;

        public ArticlesControllerTests()
        {
            dbContext = DatabaseMock.Instance;
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
            service = new ArticleService(dbContext, mapper);
            controller = new ArticlesController(service);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        [Fact]
        public void All_ShouldReturnView()
        {
            // Arrange
            dbContext
                .Articles
                .Add(new Article
                {
                    Title = "testTitle",
                    Content = "testContent",
                    ArticleType = ArticleType.Article,
                    ImageUrl = "testImageUrl",
                    CreatorName = "testCreatorName"
                });

            // Act
            var result = controller.All();

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeOfType<ViewResult>();
        } 

        [Fact]
        public void Read_ShouldReturnView()
        {
            // Arrange
            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = "testTitle",
                    Content = "testContent",
                    ArticleType = ArticleType.Article,
                    ImageUrl = "testImageUrl",
                    CreatorName = "testCreatorName"
                });

            // Act
            var result = controller.Read(articleId);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void CreateGet_ShouldReturnView()
        {
            // Arrange
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = ClaimsPrincipalMock.Instance()
            };

            // Act
            var result = controller.Create();

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void CreatePost_ShouldReturnRedirectToAction()
        {
            // Arrange
            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = ClaimsPrincipalMock.Instance()
            };

            var model = new ArticleFormModel
            {
                Title = "testTitle",
                Content = "testContent",
                ImageUrl = "testUrl",
                CreatorName = "TestUser",
                ArticleType = "Article",
                ArticlesTypes = new List<string>()
            };

            // Act
            var result = controller.Create(model);

            // Assert
            result
                .Should()
                .NotBeNull()
                .And
                .BeOfType<RedirectToActionResult>();
        }
    }
}
