namespace HighPaw.Services.Article
{
    using HighPaw.Services.Article.Models;
    using System.Collections.Generic;

    public interface IArticleService
    {
        public int Create(
            string title,
            string content,
            string imageUrl,
            string creatorName,
            string articleType);

        public ArticleServiceModel Read(int id);

        public void Delete(int id);

        public IEnumerable<ArticleServiceModel> All();
    }
}
