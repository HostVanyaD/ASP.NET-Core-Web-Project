namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Event;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : BaseController
    {
        private readonly IEventService events;

        public EventsController(IEventService events)
            => this.events = events;

        public IActionResult All()
        {
            var allArticles = this.events.All();

            return View(allArticles);
        }

        public IActionResult Delete(int id)
        {
            this.events.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
