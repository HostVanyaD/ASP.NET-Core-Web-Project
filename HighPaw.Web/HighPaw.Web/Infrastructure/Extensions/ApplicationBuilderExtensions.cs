namespace HighPaw.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
               this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedDatabase(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<HighPawDbContext>();

            data.Database.Migrate();
        }

        private static void SeedDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<HighPawDbContext>();

            if (!data.SizeCategories.Any())
            {
                data.SizeCategories.AddRange(new List<SizeCategory>()
                    {
                        new SizeCategory() // 1
                        {
                           Name = "Small",
                           Description = "<= 10kg"
                        },
                        new SizeCategory() // 2
                        {
                           Name = "Medium",
                           Description = "10kg > 25kg"
                        },
                        new SizeCategory() // 3
                        {
                           Name = "Large",
                           Description = "25kg > 45kg"
                        },
                        new SizeCategory() // 4
                        {
                           Name = "Extra Large",
                           Description = "> 45kg"
                        }
                    });
            }

            if (!data.Shelters.Any())
            {
                data.Shelters.AddRange(new List<Shelter>()
                    {
                        new Shelter() // 1
                        {
                           Name = "Animal Hope Varna",
                           Address = "Levski 9000, Varna, Bulgaria",
                           Email = "ANIMAL.HOPE.VARNA.BG@GMAIL.COM",
                           PhoneNumber = "+35952820603",
                           Website = "https://animalhope-varna.org/",
                           Description = "The Animal Hope Foundation is a non-profit organization that seeks to raise awareness against cruelty to animals and to provide assistance to the homeless animals in the city of Varna."
                        },
                        new Shelter() // 2
                        {
                           Name = "OPBK",
                           Address = "9102 Kamenar, Varna Province",
                           Email = "opbk@varna.bg",
                           PhoneNumber = "+35952820603",
                           Website = "https://opbk.varna.bg/",
                           Description = "The activity of the municipal shelter for stray dogs is carried out on the territory of the municipality of Varna."
                        },
                        new Shelter() // 3
                        {
                           Name = "Four Paws",
                           Address = "Pirotska 8, Sofia",
                           Email = "office@four-paws.bg",
                           PhoneNumber = "+35929531784",
                           Website = "https://www.four-paws.bg",
                           Description = "FOUR PAWS is the strong, global and independent voice of animals that depend directly on humans. Our vision is a better world in which animals are not subjected to suffering and are raised with respect and understanding."
                        },
                    });
            }

            if (!data.Pets.Any())
            {
                data.Pets.AddRange(new List<Pet>()
                    {
                        new Pet()
                        {
                            Name = "Nikki",
                            ImageUrl = "https://pet-uploads.adoptapet.com/c/8/4/582815365.jpg",
                            PetType = PetType.Dog,
                            Breed = "Mix",
                            Age = 8,
                            Color = "Brown/Chocolate",
                            SizeCategoryId = 3,
                            Sex = "Female",
                            ShelterId = 3
                        },
                        new Pet()
                        {
                            Name = "Daisy",
                            ImageUrl = "https://pet-uploads.adoptapet.com/e/e/8/604468416.jpg",
                            PetType = PetType.Cat,
                            Breed = "Domestic Mediumhair",
                            Age = 1,
                            Color = "White",
                            SizeCategoryId = 1,
                            Sex = "Female",
                            ShelterId = 1
                        },
                        new Pet()
                        {
                            Name = "Penny",
                            ImageUrl = "https://pet-uploads.adoptapet.com/a/2/2/611272413.jpg",
                            PetType = PetType.Dog,
                            Breed = "German Shepherd Mix",
                            Age = 4,
                            Color = "Brown/Chocolate",
                            SizeCategoryId = 3,
                            Sex = "Female",
                            ShelterId = 2
                        },
                        new Pet()
                        {
                            Name = "Winston",
                            ImageUrl = "https://pet-uploads.adoptapet.com/b/f/1/610269284.jpg",
                            PetType = PetType.Dog,
                            Breed = "Husky/German Shepherd Dog Mix",
                            Age = 2,
                            Color = "Black/White",
                            SizeCategoryId = 3,
                            Sex = "Male",
                            ShelterId = 1
                        },
                        new Pet()
                        {
                            Name = "Riley",
                            ImageUrl = "https://pet-uploads.adoptapet.com/e/c/e/237019547.jpg",
                            PetType = PetType.Dog,
                            Breed = "American Bulldog/Pit Bull Terrier Mix",
                            Age = 7,
                            Color = "Black/White",
                            SizeCategoryId = 2,
                            Sex = "Female",
                            ShelterId = 2
                        },
                        new Pet()
                        {
                            Name = "Dodger",
                            ImageUrl = "https://pet-uploads.adoptapet.com/f/1/0/611951010.jpg",
                            PetType = PetType.Dog,
                            Breed = "German Shepherd",
                            Age = 10,
                            Color = "Brown/Chocolate",
                            SizeCategoryId = 3,
                            Sex = "Male",
                            ShelterId = 3
                        },
                        new Pet()
                        {
                            Name = "Dhalia",
                            ImageUrl = "https://pet-uploads.adoptapet.com/4/0/b/612024089.jpg",
                            PetType = PetType.Cat,
                            Breed = "Domestic Shorthair",
                            Age = 1,
                            Color = "White",
                            SizeCategoryId = 1,
                            Sex = "Female",
                            ShelterId = 3
                        },
                        new Pet()
                        {
                            Name = "Lucifer",
                            ImageUrl = "https://pet-uploads.adoptapet.com/f/0/b/611506164.jpg",
                            PetType = PetType.Cat,
                            Breed = "Domestic Shorthair",
                            Age = 1,
                            Color = "Orange/white",
                            SizeCategoryId = 1,
                            Sex = "Male",
                            ShelterId = 1
                        },
                    });
            }

            data.SaveChanges();
        }
    }
}