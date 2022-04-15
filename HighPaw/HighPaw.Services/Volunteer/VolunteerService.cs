namespace HighPaw.Services.Volunteer
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using static HighPaw.Services.GlobalConstants;

    public class VolunteerService : IVolunteerService
    {
        private readonly HighPawDbContext data;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public VolunteerService(
            HighPawDbContext data,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public bool IsVolunteer(string id)
            => this.data
                .Volunteers
                .Any(v => v.UserId == id);

        public int Become(
            string firstName, 
            string lastName, 
            string email, 
            string allAboutYou,
            string userId)
        {
            var volunteer = new Volunteer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                AllAboutYou = allAboutYou,
                UserId = userId
            };

            this.data.Volunteers.Add(volunteer);

            Task
                .Run(async () =>
                {
                    var user = await userManager.FindByIdAsync(userId);

                    await userManager.AddToRoleAsync(user, VolunteerRoleName);

                    await data.SaveChangesAsync();

                    await signInManager.RefreshSignInAsync(user);
                })
                .GetAwaiter()
                .GetResult();

            return volunteer.Id;
        }
    }
}
