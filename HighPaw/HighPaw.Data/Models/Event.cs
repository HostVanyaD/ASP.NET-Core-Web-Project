namespace HighPaw.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.EventAndArticle;

    public class Event
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}
