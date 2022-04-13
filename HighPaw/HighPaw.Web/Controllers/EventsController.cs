namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Event;
    using HighPaw.Web.Models.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Areas.Admin.AdminConstants;
    using static HighPaw.Services.GlobalConstants;

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

        [Authorize(Roles = $"{AdminRoleName}, {VolunteerRoleName}")]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Authorize(Roles = $"{AdminRoleName}, {VolunteerRoleName}")]
        public IActionResult Create(EventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var eventId = this.events
                .Create(
                model.Title,
                model.Description,
                model.Location,
                model.Date);

            return RedirectToAction(nameof(All));
        }
    }
}
