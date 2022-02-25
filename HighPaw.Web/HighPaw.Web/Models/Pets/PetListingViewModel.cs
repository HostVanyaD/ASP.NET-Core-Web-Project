namespace HighPaw.Web.Models.Pets
{
    public class PetListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; set; }

        public int? Age { get; set; }

        public string Sex { get; set; }

        public string Shelter { get; set; }
    }
}
