namespace HighPaw.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HighPaw.Data.Models.Enums;
    using static DataConstants.Pet;

    public class Pet
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public PetType PetType { get; set; }

        [Required]
        [MaxLength(BreedMaxLength)]
        public string Breed { get; set; }

        public int? Age { get; set; }

        [Required]
        [MaxLength(GenderMaxLength)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }


        [MaxLength(MicrochipIdMaxLength)]
        public string MicrochipId { get; set; }

        public bool IsAdopted { get; set; } = false;

        public bool IsLost { get; set; } = false;

        public string LastSeenLocation { get; set; }

        public DateTime? LostDate { get; set; }

        public bool IsFound { get; set; } = false;

        public string FoundLocation { get; set; }

        public DateTime? FoundDate { get; set; }


        public int SizeCategoryId { get; set; }
        public SizeCategory SizeCategory { get; set; }


        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}
