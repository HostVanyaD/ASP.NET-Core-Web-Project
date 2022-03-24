namespace HighPaw.Services.Pet
{

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Services.Pet.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PetService : IPetService
    {
        private readonly HighPawDbContext data;
        private readonly IConfigurationProvider mapper;

        public PetService(HighPawDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IEnumerable<LatestPetServiceModel> Latest()
            => this.data
                .Pets
                .OrderByDescending(c => c.Id)
                .ProjectTo<LatestPetServiceModel>(this.mapper)
                .Take(3)
                .ToList();
    }
}
