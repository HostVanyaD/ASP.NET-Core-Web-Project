namespace HighPaw.Services.Pet
{
    using HighPaw.Services.Pet.Models;
    using System.Collections.Generic;

    public interface IPetService
    {
        public IEnumerable<LatestPetServiceModel> Latest();
    }
}
