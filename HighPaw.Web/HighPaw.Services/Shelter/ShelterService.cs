namespace HighPaw.Services.Shelter
{
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HighPaw.Data;
    using HighPaw.Data.Models;
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

        public IEnumerable<ShelterNameServiceModel> GetAll()
            => this.data
                .Shelters
                .ProjectTo<ShelterNameServiceModel>(this.mapper)
                .ToList();

        public int Add(
            string name, 
            string address, 
            string email,
            string phoneNumber, 
            string description, 
            string website)
        {
            var shelterData = new Shelter
            {
                Name = name,
                Address = address,
                Email = email,
                PhoneNumber = phoneNumber,
                Description = description,
                Website = website
            };

            this.data.Shelters.Add(shelterData);
            this.data.SaveChanges();

            return shelterData.Id;
        }
    }
}
