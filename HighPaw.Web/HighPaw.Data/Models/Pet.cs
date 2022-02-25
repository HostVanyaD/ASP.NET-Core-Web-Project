namespace HighPaw.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HighPaw.Data.Models.Enums;
    using static DataConstants;

    public class Pet
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(PetNameMaxLength)]
        public string Name { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public PetType PetType { get; set; }

        [Required]
        [MaxLength(BreedMaxLength)]
        public string Breed { get; set; }

        public int? Age { get; set; }

        [Required]
        [MaxLength(SexMaxLength)]
        public string Sex { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }

        [MaxLength(MicrochipIdMaxLength)]
        public string MicrochipId { get; set; }

        public bool IsAdopted { get; set; } = false;

        public bool IsLost { get; set; } = false;

        public bool IsFound { get; set; } = false;

        public int SizeCategoryId { get; set; }
        public SizeCategory SizeCategory { get; set; }

        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
    }
}
