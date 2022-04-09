namespace HighPaw.Services.Pet.Models
{
    public interface IPetModel
    {
        public string Name { get; }

        public string ImageUrl { get; }

        public int? Age { get; }

        public string Gender { get; }

        public string ShelterName { get; }
    }
}
