namespace HighPaw.Web.Models.Shelters
{
    public class ShelterDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }
    }
}
