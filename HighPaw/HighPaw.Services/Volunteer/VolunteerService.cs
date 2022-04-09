namespace HighPaw.Services.Volunteer
{
    using System.Linq;
    using HighPaw.Data;
    using HighPaw.Data.Models;

    public class VolunteerService : IVolunteerService
    {
        private readonly HighPawDbContext data;

        public VolunteerService(HighPawDbContext data)
            => this.data = data;

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
            this.data.SaveChanges();

            return volunteer.Id;
        }
    }
}
