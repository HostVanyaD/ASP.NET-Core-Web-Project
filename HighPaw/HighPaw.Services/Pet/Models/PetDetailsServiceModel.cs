namespace HighPaw.Services.Pet.Models
{
    using HighPaw.Services.Shelter.Models;

    public class PetDetailsServiceModel
    {
        public int Id { get; init; }

        public string PetType { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public string MicrochipId { get; set; }

        public string FoundLocation { get; set; }

        public string LastSeenLocation { get; set; }

        public string FoundDate { get; set; }

        public string LostDate { get; set; }

        public bool IsAdopted { get; set; } = false;

        public bool IsLost { get; set; } = false;

        public bool IsFound { get; set; } = false;

        public string SizeCategory { get; set; }

        public ShelterServiceModel Shelter { get; set; }
    }
}
