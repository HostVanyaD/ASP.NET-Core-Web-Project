namespace HighPaw.Services.Article.Models
{
    public class ArticleServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string CreatorName { get; set; }

        public string CreatedOn { get; set; }

        public string ArticleType { get; set; }
    }
}
