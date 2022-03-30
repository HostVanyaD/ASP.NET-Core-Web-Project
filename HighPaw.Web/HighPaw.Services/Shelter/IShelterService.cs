namespace HighPaw.Services.Shelter
{
    using HighPaw.Services.Shelter.Models;
    using System.Collections.Generic;

    public interface IShelterService
    {
        public IEnumerable<ShelterServiceModel> GetAll();

        public IEnumerable<ShelterNameServiceModel> GetAllNames();

        public int Add(
            string name,
            string address,
            string email,
            string phoneNumber,
            string description,
            string website);
    }
}
