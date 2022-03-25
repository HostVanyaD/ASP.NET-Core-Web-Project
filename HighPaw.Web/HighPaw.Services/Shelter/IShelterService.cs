namespace HighPaw.Services.Shelter
{
    using HighPaw.Services.Shelter.Models;
    using System.Collections.Generic;

    public interface IShelterService
    {
        public IEnumerable<ShelterNameServiceModel> AllShelters();
    }
}
