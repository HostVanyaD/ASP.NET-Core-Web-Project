namespace HighPaw.Web.Models.Articles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static HighPaw.Data.DataConstants.EventAndArticle;

    public class ArticleFormModel
    {
        [Required]
        [StringLength(
            TitleMaxLength,
            MinimumLength = TitleMinLength,
            ErrorMessage = TitleErrorMessage)]
        public string Title { get; init; }

        [Required]
        [StringLength(
            ContentMaxLength,
            MinimumLength = ContentMinLength,
            ErrorMessage = ContentErrorMessage)]
        public string Content { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public string CreatorName { get; set; }

        [Required]
        public string ArticleType { get; set; }

        public IEnumerable<string> ArticlesTypes { get; set; }
    }
}
