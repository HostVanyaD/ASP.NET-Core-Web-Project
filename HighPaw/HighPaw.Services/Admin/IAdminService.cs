namespace HighPaw.Services.Admin
{
    using HighPaw.Services.Admin.Models;
    using System.Collections.Generic;

    public interface IAdminService
    {
        public List<AdminPetListingServiceModel> GetLatestPets();

        public List<AdminArticleListingServiceModel> GetLatestArticles();
    }
}
