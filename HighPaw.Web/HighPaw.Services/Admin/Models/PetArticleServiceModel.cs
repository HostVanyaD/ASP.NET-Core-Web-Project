namespace HighPaw.Services.Admin.Models
{
    using System.Collections.Generic;

    public class PetArticleServiceModel
    {
        public List<AdminPetListingServiceModel> Pets { get; set; }

        public List<AdminArticleListingServiceModel> Articles { get; set; }
    }
}
