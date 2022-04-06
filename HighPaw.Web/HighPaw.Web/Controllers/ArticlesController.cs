namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Article;
    using HighPaw.Web.Infrastructure.Extensions;
    using HighPaw.Web.Models.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static HighPaw.Services.GlobalConstants;

    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;

        public ArticlesController(IArticleService articles)
            => this.articles = articles;

        public IActionResult All()
        {
            var allArticles = this.articles
                .All();

            return View(allArticles);
        }

        public IActionResult Read(int id)
        {
            var article = this.articles.Read(id);

            return View(article);
        }

        [Authorize]
        public IActionResult Create()
        {
            string userFullName = User.FullName();

            return View(new ArticleFormModel
            {
                CreatorName = userFullName,
                ArticlesTypes = new[] { ArticleArticleType, StoryArticleType }
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ArticleFormModel article)
        {
            string userFullName = User.FullName();

            if (article.CreatorName != userFullName)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Create));
            }

            var articleId = this.articles
                .Create(
                article.Title,
                article.Content,
                article.ImageUrl,
                article.CreatorName,
                article.ArticleType);

            return RedirectToAction(nameof(All));
        }
    }
}
