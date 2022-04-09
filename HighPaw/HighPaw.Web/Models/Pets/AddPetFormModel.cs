namespace HighPaw.Web.Models.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HighPaw.Services.Pet.Models;
    using HighPaw.Services.Shelter.Models;
    using static HighPaw.Data.DataConstants.Pet;

    public class AddPetFormModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = NameErrorMessage)]
        public string Name { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public string PetType { get; set; }

        [Required]
        [StringLength(
             BreedMaxLength,
             MinimumLength = BreedMinLength,
             ErrorMessage = BreedErrorMessage)]
        public string Breed { get; set; }

        [Range(AgeMinValue, AgeMaxValue)]
        public int? Age { get; set; }

        //[MinLength(GenderMinLength)]
        //[MaxLength(GenderMaxLength)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(ColorMaxLength)]
        public string Color { get; set; }

        [StringLength(
             MicrochipIdMaxLength,
             MinimumLength = MicrochipIdMinLength,
             ErrorMessage = MicrochipErrorMessage)]
        public string MicrochipId { get; set; }

        public bool IsLost { get; set; } = false;

        public string LastSeenLocation { get; set; }

        public DateTime? LostDate { get; set; }

        public bool IsFound { get; set; } = false;

        public string FoundLocation { get; set; }

        public DateTime? FoundDate { get; set; }

        [Display(Name = "Size Category")]
        public int SizeCategoryId { get; set; }

        [Display(Name = "Shelter")]
        public int ShelterId { get; set; }

        public IEnumerable<SizeCategoryServiceModel> Categories { get; set; }
        public IEnumerable<ShelterNameServiceModel> Shelters { get; set; }
    }
}
