namespace HighPaw.Web.Models.Volunteers
{
    using System.ComponentModel.DataAnnotations;
    using static HighPaw.Data.DataConstants.Volunteer;

    public class BecomeVolunteerFormModel
    {
        [Required]
        [StringLength(
            FirstNameMaxLength,
            MinimumLength = DefaultNameMinLength,
            ErrorMessage = FirstNameErrorMessage)]
        public string FirstName { get; init; }

        [Required]
        [StringLength(
            LastNameMaxLength,
            MinimumLength = DefaultNameMinLength,
            ErrorMessage = LastNameErrorMessage)]
        public string LastName { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [Display(Name = "About you")]
        public string AllAboutYou { get; set; }
    }
}
