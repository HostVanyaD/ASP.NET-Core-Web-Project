namespace HighPaw.Services.Article
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Article.Models;

    public class ArticleService : IArticleService
    {
        private readonly HighPawDbContext data;
        private readonly IConfigurationProvider mapper;

        public ArticleService(HighPawDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<ArticleServiceModel> All()
            => this.data
                .Articles
                .OrderByDescending(a => a.Id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .ToList();

        public int Create(
            string title, 
            string content, 
            string imageUrl, 
            string creatorName, 
            string articleType)
        {
            var articleData = new Article
            {
                Title = title,
                Content = content,
                ImageUrl = imageUrl,
                CreatorName = creatorName,
                ArticleType = Enum.Parse<ArticleType>(articleType)
            };

            this.data.Articles.Add(articleData);
            this.data.SaveChanges();

            return articleData.Id;
        }

        public void Edit(ArticleServiceModel model)
        {
            var article = this.data
                .Articles
                .FirstOrDefault(e => e.Id == model.Id);

            article.Title = model.Title;
            article.Content = model.Content;
            article.ImageUrl = model.ImageUrl;
            article.CreatorName = model.CreatorName;
            article.ArticleType = Enum.Parse<ArticleType>(model.ArticleType);
            article.CreatedOn = DateTime.Parse(model.CreatedOn);

            this.data.Update(article);
            this.data.SaveChanges();
        }

        public ArticleServiceModel GetById(int? id)
            => this.data
                .Articles
                .Where(e => e.Id == id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool DoesExist(int id)
            => this.data
                .Articles
                .Any(e => e.Id == id);

        public ArticleServiceModel Read(int id)
            => this.data
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleServiceModel>(this.mapper)
                .FirstOrDefault();

        public void Delete(int id)
        {
            var articleToDelete = this.data
                .Articles
                .Find(id);

            this.data
                .Articles
                .Remove(articleToDelete);

            this.data.SaveChanges();
        }
    }
}
