﻿namespace HighPaw.Web.Models.Pets
{
    using HighPaw.Web.Models.Shelters;

    public class PetDetailsViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; set; }

        public string PetType { get; set; }

        public string Breed { get; set; }

        public int? Age { get; set; }

        public string Sex { get; set; }

        public string Color { get; set; }

        public string MicrochipId { get; set; }

        public string FoundLocation { get; set; }

        public bool IsAdopted { get; set; } = false;

        public bool IsLost { get; set; } = false;

        public bool IsFound { get; set; } = false;

        public string SizeCategory { get; set; }

        public ShelterDetailsViewModel Shelter { get; set; }
    }
}
