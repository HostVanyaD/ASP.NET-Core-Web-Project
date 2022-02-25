namespace HighPaw.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;

    public class Event
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; init; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Location { get; set; }
    }
}
