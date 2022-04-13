namespace HighPaw.Web.Models.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static HighPaw.Data.DataConstants.EventAndArticle;

    public class EventFormModel
    {

        [Required]
        [StringLength(
            TitleMaxLength,
            MinimumLength = TitleMinLength,
            ErrorMessage = TitleErrorMessage)]
        public string Title { get; init; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(
            AddressMaxLength,
            ErrorMessage = AddressErrorMessage)]
        public string Location { get; set; }

        public DateTime Date { get; set; }
    }
}
