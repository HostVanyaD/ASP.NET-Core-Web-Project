namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Event;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly IEventService events;

        public EventsController(IEventService events)
            => this.events = events;

        public IActionResult All()
        {
            var events = this.events.All();

            return View(events);
        }
    }
}
