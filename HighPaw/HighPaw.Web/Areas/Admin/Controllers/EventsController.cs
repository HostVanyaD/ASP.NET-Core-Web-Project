namespace HighPaw.Web.Areas.Admin.Controllers
{
    using HighPaw.Services.Event;
    using HighPaw.Services.Event.Models;
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

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var model = this.events
                .GetById(id);

            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EventServiceModel model)
        {
            if (!this.events.DoesExist(model.Id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.events.Edit(model);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            this.events.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
