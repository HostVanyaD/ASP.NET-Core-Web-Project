namespace HighPaw.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Shelter;

    public class Shelter
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Required]
        [MaxLength(AddressMaxLength)]
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

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();
    }
}