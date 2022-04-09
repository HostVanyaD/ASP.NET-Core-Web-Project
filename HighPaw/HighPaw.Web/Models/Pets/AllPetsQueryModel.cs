namespace HighPaw.Web.Models.Pets
{
    using System.Collections.Generic;
    using HighPaw.Services.Pet.Models;

    public class AllPetsQueryModel : QueryViewModel
    {
        public IEnumerable<PetListingServiceModel> Pets { get; set; }
    }
}
