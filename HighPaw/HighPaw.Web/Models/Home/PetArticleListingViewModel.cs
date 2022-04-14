namespace HighPaw.Web.Models.Home
{
    using System.Collections.Generic;
    using HighPaw.Services.Article.Models;
    using HighPaw.Services.Pet.Models;

    public class PetArticleListingViewModel
    {
        public IEnumerable<PetListingServiceModel> LatestPets { get; set; }

        public IEnumerable<ArticleServiceModel> LatestArticles { get; set; }
    }
}
