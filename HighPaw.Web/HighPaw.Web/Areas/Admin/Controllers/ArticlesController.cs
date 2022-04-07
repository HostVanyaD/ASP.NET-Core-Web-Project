namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Article;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articles;

        public ArticlesController(IArticleService articles)
            => this.articles = articles;

        public IActionResult All()
        {
            var allArticles = this.articles.All();

            return View(allArticles);
        }

        public IActionResult Delete(int id)
        {
            this.articles.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
