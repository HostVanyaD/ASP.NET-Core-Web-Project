namespace HighPaw.Services.Shelter
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Services.Shelter.Models;

    public class ShelterService : IShelterService
    {
        private readonly HighPawDbContext data;
        private readonly IConfigurationProvider mapper;

        public ShelterService(HighPawDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }
        public IEnumerable<ShelterNameServiceModel> AllShelters()
            => this.data
                .Shelters
                .ProjectTo<ShelterNameServiceModel>(this.mapper)
                .ToList();
    }
}
