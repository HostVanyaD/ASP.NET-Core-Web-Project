namespace HighPaw.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HighPaw.Data.Models.Enums;
    using static DataConstants;

    public class Article
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; init; }

        [Required]
        public string Content { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(CreatorNameMaxLength)]
        public string CreatorName { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ArticleType ArticleType { get; set; }
    }
}
