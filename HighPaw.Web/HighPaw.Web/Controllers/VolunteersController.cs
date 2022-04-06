namespace HighPaw.Web.Controllers
{
    using HighPaw.Services.Volunteer;
    using HighPaw.Web.Infrastructure.Extensions;
    using HighPaw.Web.Models.Volunteers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class VolunteersController : Controller
    {
        private readonly IVolunteerService volunteers;

        public VolunteersController(IVolunteerService volunteers)
            => this.volunteers = volunteers;


        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeVolunteerFormModel model)
        {
            var userId = User.Id();

            var userIdAlreadyAVolunteer = this.volunteers
                .IsVolunteer(userId);

            if (userIdAlreadyAVolunteer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.volunteers.Become(model.FirstName, model.LastName, model.Email, model.AllAboutYou, userId);

            ViewData["VolunteersMessage"] = "We will contact you with more details.";

            //TODO: Create a toast with VolunteersMessage

            return RedirectToAction("Index", "Home");
        }
    }
}
