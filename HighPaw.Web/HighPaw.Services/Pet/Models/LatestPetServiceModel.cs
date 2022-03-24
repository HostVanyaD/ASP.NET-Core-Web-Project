namespace HighPaw.Services.Pet.Models
{
    public class LatestPetServiceModel : IPetModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public int? Age { get; init; }

        public string Gender { get; init; }

        public string ShelterName { get; init; }
    }
}
