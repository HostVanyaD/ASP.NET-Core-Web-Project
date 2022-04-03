namespace HighPaw.Services.Pet.Models
{
    using System.Collections.Generic;

    public class PetQueryServiceModel : QueryServiceModel<PetListingServiceModel>
    {
        public override IEnumerable<string> Filters => new List<string>() { "Breed", "Age", "Gender" };
    }
}
