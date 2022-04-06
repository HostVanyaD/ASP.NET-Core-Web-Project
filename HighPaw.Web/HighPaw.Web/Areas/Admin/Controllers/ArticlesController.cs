namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Article;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : BaseController
    {
        private readonly IArticleService articles;

        public ArticlesController(IArticleService articles)
            => this.articles = articles;

        public IActionResult Delete(int id)
        {
            return View(); //TODO:
        }
    }
}
