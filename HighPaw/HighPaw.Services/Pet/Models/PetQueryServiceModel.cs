namespace HighPaw.Services.Pet.Models
{
    public class PetQueryServiceModel : QueryServiceModel<PetListingServiceModel>
    {
        public string Filters { get; set; }
    }
}
