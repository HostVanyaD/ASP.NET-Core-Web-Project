namespace HighPaw.Web.Controllers
{
    using HighPaw.Data.Models;
    using HighPaw.Services.Article;
    using HighPaw.Web.Infrastructure.Extensions;
    using HighPaw.Web.Models.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using static HighPaw.Services.GlobalConstants;

    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;
        private readonly UserManager<User> userManager;

        public ArticlesController(
            IArticleService articles,
            UserManager<User> userManager)
        {
            this.articles = articles;
            this.userManager = userManager;
        }

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
        public async Task<IActionResult> Create()
        {
            string userFullName = await GetCurentUserFullName();

            return View(new ArticleFormModel
            {
                CreatorName = userFullName,
                ArticlesTypes = new[] { ArticleArticleType, StoryArticleType }
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ArticleFormModel article)
        {
            string userFullName = await GetCurentUserFullName();

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

        private async Task<string> GetCurentUserFullName()
        {
            var userId = User.Id();
            var user = await userManager.FindByIdAsync(userId);
            var userFullName = user.FullName;
            return userFullName;
        }
    }
}
