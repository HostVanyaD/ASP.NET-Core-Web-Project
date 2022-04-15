namespace HighPaw.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using AutoMapper;
    using FluentAssertions;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Article;
    using HighPaw.Services.Article.Models;
    using HighPaw.Web.Infrastructure;
    using HighPaw.Tests.Mocks;

    public class ArticleServiceTests : IDisposable
    {
        private const string testTitle = "Title";
        private const string testContent = "Content";
        private const ArticleType testArticleType = ArticleType.Article;
        private const string testImageUrl = "image";
        private const string testCreatorName = "Test";


        private readonly Data.HighPawDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ArticleService service;

        public ArticleServiceTests()
        {
            dbContext = DatabaseMock.Instance;
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);
            service = new ArticleService(dbContext, mapper);
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
                .Articles
                .AddRange(new List<Article>
                {
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    },
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
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
        [InlineData(testTitle, testContent, testImageUrl, testCreatorName, "Article")]
        public void Create_ShouldCreateNewArticleAndAddItToDb_AndReturnItsId(
            string title, string content, string imageUrl, string creatorName, string articleType)
        {
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
            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = testTitle,
                    Content = testContent,
                    ArticleType = testArticleType,
                    ImageUrl = testImageUrl,
                    CreatorName = testCreatorName
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
            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = testTitle,
                    Content = testContent,
                    ArticleType = testArticleType,
                    ImageUrl = testImageUrl,
                    CreatorName = testCreatorName
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

        [Fact]
        public void Latest_ShouldReturnCollectionOfLatestArticles()
        {
            // Arrange
            dbContext
                .Articles
                .AddRange(new List<Article>
                {
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    },
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    },
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    },
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    },
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    },
                    new Article
                    {
                        Title = testTitle,
                        Content = testContent,
                        ArticleType = testArticleType,
                        ImageUrl = testImageUrl,
                        CreatorName = testCreatorName
                    }
                });

            dbContext.SaveChanges();

            // Act
            var result = service.Latest();

            // Assert
            result
                .Should()
                .HaveCount(6)
                .And
                .AllBeOfType<ArticleServiceModel>();
        }

        [Fact]
        public void Edit_ShouldUpdateAnArticle()
        {
            // Arrange
            var articleId = 1;

            var initialModel = new Article
            {
                Id = articleId,
                Title = testTitle,
                Content = testContent,
                ArticleType = testArticleType,
                ImageUrl = testImageUrl,
                CreatorName = testCreatorName
            };

            dbContext
                .Articles
                .Add(initialModel);

            dbContext.SaveChanges();

            var editModel = new ArticleServiceModel
            {
                Id = articleId,
                Title = testTitle + "New",
                Content = testContent + "New",
                ArticleType = "Article",
                ImageUrl = testImageUrl,
                CreatorName = testCreatorName,
                CreatedOn = "2022-3-30"
            };

            // Act
            service.Edit(editModel);

            // Assert
            initialModel
                .Title
                .Should()
                .BeEquivalentTo(editModel.Title);

            initialModel
                .Content
                .Should()
                .BeEquivalentTo(editModel.Content);
        }

        [Fact]
        public void GetById_ShouldReturnCorrectArticleIsPresent()
        {
            // Arrange
            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = testTitle,
                    Content = testContent,
                    ArticleType = testArticleType,
                    ImageUrl = testImageUrl,
                    CreatorName = testCreatorName
                });

            dbContext.SaveChanges();

            // Act
            var result = service.GetById(articleId);

            // Assert
            result
                .Id
                .Should()
                .Be(articleId);
        }

        [Fact]
        public void DoesExist_ShouldReturnTrueIfArticleIsPresent()
        {
            // Arrange
            var articleId = 1;

            dbContext
                .Articles
                .Add(new Article
                {
                    Id = articleId,
                    Title = testTitle,
                    Content = testContent,
                    ArticleType = testArticleType,
                    ImageUrl = testImageUrl,
                    CreatorName = testCreatorName
                });

            dbContext.SaveChanges();

            // Act
            var result = service.DoesExist(articleId);

            // Assert
            result
                .Should()
                .BeTrue();
        }
    }
}
