namespace HighPaw.Data
{
    public static class DataConstants
    {
        public class Pet
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 20;
            public const int BreedMinLength = 2;
            public const int BreedMaxLength = 50;
            public const int MicrochipIdMinLength = 9;
            public const int MicrochipIdMaxLength = 15;
            public const int AgeMinValue = 0;
            public const int AgeMaxValue = 30;
            public const int GenderMinLength = 4;
            public const int GenderMaxLength = 6;
            public const int ColorMaxLength = 20;

            public const string NameErrorMessage = "Name must be between {2} and {1} characters long.";
            public const string BreedErrorMessage = "Breed must be between {2} and {1} characters long.";
            public const string MicrochipErrorMessage = "Microchip must be between {2} and {1} characters long.";

        }

        public class Shelter
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
            public const int AddressMaxLength = 255;
            public const int PhoneMaxLength = 15;

        }

        public class Volunteer
        {
            public const int DefaultNameMinLength = 2;
            public const int FirstNameMaxLength = 20;
            public const int LastNameMaxLength = 60;
        }

        public class EventAndArticle
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 80;
            public const int ContentMinLength = 100;
            public const int ContentMaxLength = 2000000000;
            public const int CreatorNameMinLength = 5;
            public const int CreatorNameMaxLength = 80;
            public const int AddressMaxLength = 255;

            public const string TitleErrorMessage = "Title must be between {2} and {1} characters long.";
            public const string ContentErrorMessage = "Content must be between {2} and {1} characters long.";
        }

        public class User
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 100;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }
    }
}
