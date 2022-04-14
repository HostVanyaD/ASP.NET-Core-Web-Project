namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Article;
    using HighPaw.Web.Infrastructure.Extensions;
    using HighPaw.Web.Models.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Areas.Admin.AdminConstants;
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

        [Authorize(Roles = $"{AdminRoleName},{VolunteerRoleName}")]
        public IActionResult Create()
        {
            string userFullName = User.FullName();

            return View(new ArticleFormModel
            {
                CreatorName = userFullName,
                ArticlesTypes = new[] { ArticleArticleType, StoryArticleType }
            });
        }

        [Authorize(Roles = $"{AdminRoleName},{VolunteerRoleName}")]
        [HttpPost]
        public IActionResult Create(ArticleFormModel article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
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
