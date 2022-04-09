namespace HighPaw.Services.Shelter.Models
{
    using System.ComponentModel.DataAnnotations;
    using static HighPaw.Data.DataConstants.Shelter; 

    public class AddShelterFormServiceModel
    {
        [Required]
        [StringLength(
            NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = NameErrorMessage)]
        public string Name { get; init; }

        [Required]
        [StringLength(
            AddressMaxLength,
            ErrorMessage = AddressErrorMessage)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        [Url]
        public string Website { get; set; }
    }
}
