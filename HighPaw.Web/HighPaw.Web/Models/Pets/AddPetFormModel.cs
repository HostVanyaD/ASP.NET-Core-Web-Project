namespace HighPaw.Web.Models.Pets
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static HighPaw.Data.DataConstants.Pet;

    public class AddPetFormModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public string PetType { get; set; }

        [Required]
        [MaxLength(BreedMaxLength)]
        public string Breed { get; set; }

        public int? Age { get; set; }

        [MaxLength(GenderMaxLength)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }


        [MaxLength(MicrochipIdMaxLength)]
        public string MicrochipId { get; set; }

        public bool IsLost { get; set; } = false;

        public string LastSeenLocation { get; set; }

        public DateTime? LostDate { get; set; }

        public bool IsFound { get; set; } = false;

        public string FoundLocation { get; set; }

        public DateTime? FoundDate { get; set; }

        public int SizeCategoryId { get; set; }

        public int ShelterId { get; set; }
    }
}
