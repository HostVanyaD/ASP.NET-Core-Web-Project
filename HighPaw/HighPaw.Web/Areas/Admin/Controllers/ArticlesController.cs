namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Article;
    using HighPaw.Services.Article.Models;
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

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var model = this.articles
                .GetById(id);

            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ArticleServiceModel model)
        {
            if (!this.articles.DoesExist(model.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.articles.Edit(model);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            this.articles.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
