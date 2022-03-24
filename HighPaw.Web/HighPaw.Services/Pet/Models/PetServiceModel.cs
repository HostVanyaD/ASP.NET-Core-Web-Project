namespace HighPaw.Services.Pet.Models
{
    public class PetServiceModel : IPetModel
    {
        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public int? Age { get; init; }

        public string Gender { get; init; }

        public string ShelterName { get; init; }
    }
}
