namespace HighPaw.Services.Volunteer
{
    public interface IVolunteerService
    {
        public bool IsVolunteer(string id);

        public int Become(
            string firstName,
            string lastName,
            string email,
            string allAboutYou,
            string userId);
    }
}
