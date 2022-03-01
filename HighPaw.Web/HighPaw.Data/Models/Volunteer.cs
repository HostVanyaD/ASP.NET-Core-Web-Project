namespace HighPaw.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Volunteer;

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

        [Required]
        public string UserId { get; set; }

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}
