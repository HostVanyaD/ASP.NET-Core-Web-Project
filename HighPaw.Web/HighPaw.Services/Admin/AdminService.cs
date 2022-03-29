namespace HighPaw.Services.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Services.Admin.Models;

    public class AdminService : IAdminService
    {
        private readonly HighPawDbContext data;
        private readonly IConfigurationProvider mapper;

        public AdminService(HighPawDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public List<AdminPetListingServiceModel> GetLatestPets()
            => this.data
                .Pets
                .OrderByDescending(p => p.Id)
                .Take(10)
                .ProjectTo<AdminPetListingServiceModel>(mapper)
                .ToList();

        public List<AdminArticleListingServiceModel> GetLatestArticles()
            => this.data
                .Articles
                .OrderByDescending(a => a.Id)
                .Take(10)
                .ProjectTo<AdminArticleListingServiceModel>(mapper)
                .ToList();
    }
}
