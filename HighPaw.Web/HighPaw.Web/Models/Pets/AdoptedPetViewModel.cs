namespace HighPaw.Web.Models.Pets
{
    public class AdoptedPetViewModel
    {
        public string Name { get; init; }

        public string ImageUrl { get; set; }

        public int? Age { get; set; }

        public string Gender { get; set; }

        public string Shelter { get; set; }


        public bool IsAdopted = true;
    }
}
