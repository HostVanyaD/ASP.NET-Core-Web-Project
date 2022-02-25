namespace HighPaw.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Volunteer
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; init; }
        
        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string AllAboutYou { get; set; }
    }
}
