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

        public IEnumerable<ShelterNameServiceModel> GetAllNames()
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

        public void Edit(ShelterServiceModel model)
        {
            var shelter = this.data
                .Shelters
                .FirstOrDefault(e => e.Id == model.Id);

            shelter.Name = model.Name;
            shelter.Address = model.Address;
            shelter.Description = model.Description;
            shelter.PhoneNumber = model.PhoneNumber;
            shelter.Email = model.Email;
            shelter.Website = model.Website;           

            this.data.Update(shelter);
            this.data.SaveChanges();
        }

        public ShelterServiceModel GetById(int? id)
            => this.data
                .Shelters
                .Where(e => e.Id == id)
                .ProjectTo<ShelterServiceModel>(this.mapper)
                .FirstOrDefault();

        public bool DoesExist(int id)
            => this.data
                .Shelters
                .Any(e => e.Id == id);

        public IEnumerable<ShelterServiceModel> All()
             => this.data
                .Shelters
                .ProjectTo<ShelterServiceModel>(this.mapper)
                .ToList();

        public void Delete(int id)
        {
            var shelterToDelete = this.data
                .Shelters
                .Find(id);

            this.data.Shelters.Remove(shelterToDelete);
            this.data.SaveChanges();
        }
    }
}
