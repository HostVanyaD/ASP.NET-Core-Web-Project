namespace HighPaw.Data
{
    public static class DataConstants
    {
        // Pet
        public const int PetNameMinLength = 2;
        public const int PetNameMaxLength = 20;
        public const int BreedMinLength = 2;
        public const int BreedMaxLength = 50;
        public const int MicrochipIdMinLength = 9;
        public const int MicrochipIdMaxLength = 15;
        public const int PetAgeMinValue = 0;
        public const int PetAgeMaxValue = 30;
        public const int SexMinLength = 4;
        public const int SexMaxLength = 6;
        public const int ColorMaxLength = 20;

        // Shelter
        public const int ShelterNameMinLength = 3;
        public const int ShelterNameMaxLength = 50;
        public const int AddressMaxLength = 255;
        public const int PhoneMaxLength = 15;

        // Volunteer
        public const int DefaultNameMinLength = 2;
        public const int FirstNameMaxLength = 20;
        public const int LastNameMaxLength = 60;

        // Event and Article
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 80;
        public const int ContentMinLength = 100;
        public const int CreatorNameMinLength = 5;
        public const int CreatorNameMaxLength = 80;
    }
}
