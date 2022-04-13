namespace HighPaw.Web.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity;
    using HighPaw.Data;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;

    using static Areas.Admin.AdminConstants;
    using static HighPaw.Services.GlobalConstants;

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
                SeedCategories(data);
            }

            if (!data.Shelters.Any())
            {
                SeedShelters(data);
            }

            if (!data.Pets.Any())
            {
                SeedPets(data);
            }

            SeedAdministrator(services);
            SeedVolunteer(services);

            if (!data.Articles.Any())
            {
                SeedArticles(data);
            }

            if (!data.Events.Any())
            {
                SeedEvents(data);
            }
        }

        private static void SeedCategories(HighPawDbContext data)
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

            data.SaveChanges();
        }

        private static void SeedShelters(HighPawDbContext data)
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

            data.SaveChanges();
        }

        private static void SeedPets(HighPawDbContext data)
        {
            data.Pets.AddRange(new List<Pet>()
                    {
                        new Pet()
                        {
                            Name = "Nikki",
                            ImageUrl = "https://cdn.pixabay.com/photo/2017/09/16/14/13/smile-2755616_1280.jpg",
                            PetType = PetType.Dog,
                            Breed = "Mix",
                            Age = 8,
                            Color = "Brown/Chocolate",
                            SizeCategoryId = 3,
                            Gender = "Female",
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
                            Gender = "Female",
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
                            Gender = "Female",
                            ShelterId = 2
                        },
                        new Pet()
                        {
                            Name = "Winston",
                            ImageUrl = "https://cdn.pixabay.com/photo/2019/02/22/22/34/dog-4014545_1280.jpg",
                            PetType = PetType.Dog,
                            Breed = "Husky/German Shepherd Dog Mix",
                            Age = 2,
                            Color = "Black/White",
                            SizeCategoryId = 3,
                            Gender = "Male",
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
                            Gender = "Female",
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
                            Gender = "Male",
                            ShelterId = 3
                        },
                        new Pet()
                        {
                            Name = "Franky",
                            ImageUrl = "https://cdn.pixabay.com/photo/2019/11/18/00/38/dog-4633734_1280.jpg",
                            PetType = PetType.Dog,
                            Breed = "Mixed aussie",
                            Age = 2,
                            Color = "White/Brown/Chocolate",
                            SizeCategoryId = 2,
                            Gender = "Male",
                            ShelterId = 2
                        },
                        new Pet()
                        {
                            Name = "Jazy",
                            ImageUrl = "https://cdn.pixabay.com/photo/2017/10/02/21/56/dog-2810484_1280.jpg",
                            PetType = PetType.Dog,
                            Breed = "Mixed",
                            Age = 2,
                            Color = "White/Chocolate",
                            SizeCategoryId = 1,
                            Gender = "Male",
                            ShelterId = 1
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
                            Gender = "Female",
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
                            Gender = "Male",
                            ShelterId = 1
                        },
                        new Pet()
                        {
                            Name = "Lilly",
                            ImageUrl = "https://cdn.pixabay.com/photo/2016/03/28/10/05/kitten-1285341_1280.jpg",
                            PetType = PetType.Cat,
                            Breed = "Domestic Shorthair",
                            Age = 1,
                            Color = "White",
                            SizeCategoryId = 1,
                            Gender = "Female",
                            ShelterId = 1
                        },
                        new Pet()
                        {
                            Name = "Molly",
                            ImageUrl = "https://cdn.pixabay.com/photo/2016/11/29/01/10/kitten-1866475_1280.jpg",
                            PetType = PetType.Cat,
                            Breed = "Domestic Shorthair",
                            Age = 1,
                            Color = "White and brown",
                            SizeCategoryId = 1,
                            Gender = "Female",
                            ShelterId = 2
                        },
                        new Pet()
                        {
                            Name = "Billy",
                            ImageUrl = "https://cdn.pixabay.com/photo/2021/08/10/18/32/cat-6536684_1280.jpg",
                            PetType = PetType.Cat,
                            Breed = "Domestic Shorthair",
                            Age = 1,
                            Color = "White and brown",
                            SizeCategoryId = 1,
                            Gender = "Male",
                            ShelterId = 2
                        },
                    });

            data.SaveChanges();
        }

        private static void SeedArticles(HighPawDbContext data)
        {
            data.Articles.AddRange(new List<Article>
            {
                new Article
                {
                    Title = "Making the shelter a happier place for animals",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/05/03/00/28/puppy-750629_1280.jpg",
                    ArticleType = ArticleType.Article,
                    CreatorName = "Admin",
                    Content = @"<p>We all want the animals in our care to be as healthy and happy as possible.To accomplish this, we must attend to both their physical and emotional needs.We protect the animals’ physical health through routine vaccination, parasite control, proper nutrition, spay/neuter and other basic medical care.We create a healthy environment for them—one that is clean and well-maintained, not crowded, kept at a comfortable temperature and with good air quality.</p>
<p></p>
<p>But how can we protect the animals’ emotional health? What are the practical components of an emotional wellness program for shelter animals?</p>
<p></p>
<p>Picture yourself far away from home, from all the things you know—deposited for unknown reasons in a strange, confining place where you don’t know anyone.What would help you cope?</p>
<p></p>
<p>You’d probably want people to be nice to you.You’d want to know what to expect, so you could adjust to the daily routine. You would want something to look forward to every day. If unpleasant things occurred in the daily routine, you would want to be able to shield yourself from them, and you would want to know when they were going to happen so you could prepare yourself, and then breathe a sigh of relief and relax afterward. You would want a comfortable space with some “creature comforts”—something familiar, something tasty, something cozy, something pleasant to look at, something nice to smell and something soothing to listen to. And it would be great to get a break each day where you could get out for a little play or quiet time, and maybe spend time with someone nice.</p>
<p></p>
<p>The Power of Prevention</p>
<p>We can provide for the emotional needs of the animals in our care and help them cope with shelter life.An emotional wellness program starts with proactive strategies to decrease stress, fear and negative experiences while promoting comfort and providing regular, positive, predictable experiences throughout an animal’s stay. Providing animals with comfortable housing, gentle handling, consistent daily routines and regularly scheduled play and exercise, mental stimulation and social companionship is crucial.</p>
<p></p>
<p>Simple, consistent positive-reinforcement-based training will also go a long way toward ensuring animals feel good during their stay. Animals need to be able to do the things cats and dogs enjoy doing; they need outlets to express their normal behaviors.Most of all, they need to know how to interact and build a trusting relationship with their caregivers, because reliable, positive social connections with us are essential for their well-being.</p>
<p></p>
<p>Obtaining a history and evaluating animals at intake helps us adapt our protocols to meet the needs of individual animals. It may not be possible to obtain an accurate history on all animals at intake, but we should aim to get as much health and behavior background as possible.Assessing an animal’s level of fear and her initial response to gentle handling will also provide important insight into her needs in the shelter environment.</p>
<p></p>
<p>At a minimum, housing must include a comfortable resting place, ensure freedom from fear and distress and allow animals to engage in normal behaviors. What’s “normal” will be different for different animals, based on their species, size, personality and individual preferences. Soft bedding should be available, both for physical comfort and so that animals can establish a familiar scent, which will help them acclimate.As much as possible, animals should remain in the same enclosure—moving to a new place is always stressful! Keeping their bedding with them throughout their stay and only laundering it when it’s soiled will help.</p>
<p></p>
<p>Reducing Fear Factors</p>
<p>The power of gentle, thoughtful handling and the regular provision of creature comforts cannot be overemphasized. Help cats to feel safe by transporting them in covered carriers so that they are not exposed. Avoid placing cat carriers on the floor, where cats may feel vulnerable or threatened.Instead, place them on a counter—cats will feel more secure perched at a safe vantage point.For dogs who do not walk willingly on a leash, coax them with a treat, provide a mat to walk on if they are afraid of slippery flooring or gently carry them if necessary.Always keep cats and dogs separated, and minimize barking and other loud noises.</p>
<p></p>
<p>Whenever possible, cats and dogs who exhibit marked fear at the time of entry should be housed in specially designated quiet areas. All cats and kittens, regardless of their demeanor, should be provided with a hiding box in their enclosure at the time of entry; the ability to hide reduces fear and stress, and helps them cope while acclimating to their new environment.For cats who are severely stressed or reactive, covering the cage front and posting signage to allow the cat time to calm down or “chill out” for several hours or even a few days can also facilitate adaptation to their new environment.</p>
<p></p>
<p>Likewise, fearful dogs and “nervous little dogs” such as toy breeds will benefit from being housed in a quiet ward away from the holding kennel—or in the quietest area within the kennel.Using a shower curtain to cover the kennel front will shield them until they have time to acclimate.When they are more comfortable, begin to open the curtain so they can see out. Installation of such temporary visual barriers often calms reactive barking and helps keep the kennel quieter and less stressful for all.Minimizing foot traffic in these areas is essential—it can be frightening to animals when strangers constantly pass by, especially in the initial days of confinement before they have a chance to get their bearings.</p>
<p></p>
<p>Shielding animals from “scary” experiences is crucial to helping them relax.Cleaning routines are often scary times for many animals. Cats can be allowed to hide in boxes while their cage is quietly tidied and food and water bowls replenished. Dogs in double-sided runs can be provided with tasty treats to occupy them on one side, while the guillotine door is closed for cleaning on the other.</p>"
                },
                new Article
                {
                    Title = "Do pets have a positive effect on your brain health?",
                    ImageUrl = "https://cdn.pixabay.com/photo/2022/03/26/09/24/dog-7092595_1280.jpg",
                    ArticleType = ArticleType.Article,
                    CreatorName = "Admin",
                    Content = @"<p>Owning a pet, like a dog or cat, especially for five years or longer, may be linked to slower cognitive decline in older adults, according to a preliminary study released today, February 23, 2022 </p>
<p></p>
<p> ""Prior studies have suggested that the human-animal bond may have health benefits like decreasing blood pressure and stress,"" said study author Tiffany Braley, MD, MS, of the University of Michigan Medical Center in Ann Arbor and a member of the American Academy of Neurology. ""Our results suggest pet ownership may also be protective against cognitive decline."" </p>
<p></p>
<p> The study looked at cognitive data from 1, 369 older adults with an average age of 65 who had normal cognitive skills at the start of the study.A total of 53 % owned pets, and 32 % were long - term pet owners, defined as those who owned pets for five years or more.Of study participants, 88 % were white, 7 % were Black, 2 % were Hispanic and 3 % were of another ethnicity or race.</p>
       <p></p>
       <p> Researchers used data from the Health and Retirement Study, a large study of Medicare beneficiaries.In that study, people were given multiple cognitive tests.Researchers used those cognitive tests to develop a composite cognitive score for each person, ranging from zero to 27.The composite score included common tests of subtraction, numeric counting and word recall.Researchers then used participants' composite cognitive scores and estimated the associations between years of pet ownership and cognitive function.</p>
        <p></p>
        <p> Over six years, cognitive scores decreased at a slower rate in pet owners.This difference was strongest among long - term pet owners.Taking into account other factors known to affect cognitive function, the study showed that long - term pet owners, on average, had a cognitive composite score that was 1.2 points higher at six years compared to non - pet owners.The researchers also found that the cognitive benefits associated with longer pet ownership were stronger for Black adults, college-educated adults and men.Braley says more research is needed to further explore the possible reasons for these associations.</p>
             <p></p>
             <p> ""As stress can negatively affect cognitive function, the potential stress-buffering effects of pet ownership could provide a plausible reason for our findings,"" said Braley. ""A companion animal can also increase physical activity, which could benefit cognitive health. That said, more research is needed to confirm our results and identify underlying mechanisms for this association."" </p>
             <p></p>
             <p> A limitation of the study was that length of pet ownership was assessed only at one time point, so information regarding ongoing pet ownership was unavailable.</p>
             <p></p>
             <p> The study will be presented at the American Academy of Neurology's 74th Annual Meeting being held in person in Seattle, April 2 to 7, 2022 and virtually, April 24 to 26, 2022.</p>
             <p></p>
             <p> The study was supported by the National Institutes of Health, the National Heart, Lung, and Blood Institute and the National Institute on Aging.</p>"
                },
                new Article
                {
                    Title = "The Tales Your Cat's Tail Tells",
                    ImageUrl = "https://cdn.pixabay.com/photo/2019/11/03/09/42/cat-4598199_1280.jpg",
                    ArticleType = ArticleType.Article,
                    CreatorName = "Admin",
                    Content = @"<p>Tail Position: High</p>
<p>When your cat holds their tail high in the air as they move about their territory, they're expressing confidence and contentment. A tail that sticks straight up signals happiness and a willingness to be friendly. And watch the tip of an erect tail. A little twitch can mean a particularly happy moment.</p>
<p></p>
<p>Tail Position: Curved Like a Question Mark</p>
<p>You might consider taking a break from your daily business to play with your cat if you notice a curve in their tail. This tail position often signals a playful mood and a cat that's ready to share some fun with you.</p>
<p></p>
<p> Tail Position: Low </p>
   <p> Watch out. A tail positioned straight down can signal stress or aggression. A lower tail is a very serious mood. However, be aware that certain breeds, such as Persians, tend to carry their tails low for no particular reason.</p>
      <p></p>
      <p> Tail Position: Tucked Away </p>
      <p> A tail curved beneath the body signals fear or submission.Something is making your cat nervous.</p>
      <p></p>
      <p> Tail Position: Puffed Up </p>
      <p> A tail resembling a pipe cleaner reflects a severely agitated, stressed and / or frightened cat trying to look bigger to ward off danger.</p>
      <p></p>
      <p> Tail Position: Whipping Motion </p>
      <p> A tail that slaps back and forth rapidly indicates both fear and aggression.Consider it a warning to stay away.</p>
      <p></p>
      <p> Tail Position: Swishing Motion </p>
      <p> A tail that sways slowly from side to side usually means your cat is focused on an object.You might see this tail position right before your cat pounces on a toy or a kibble of cat food that's tumbled outside the food bowl.</p>
      <p></p>
      <p> Tail Position: Wrapped Around Another Cat </p>
      <p> A tail wrapped around another cat is like you putting your arm around another person.It conveys friendship.</p>"
                },
                new Article
                {
                    Title = "Why Do Cats Lick Their Paws? Can it Become Excessive?",
                    ImageUrl = "https://cdn.pixabay.com/photo/2014/04/13/20/49/cat-323262_1280.jpg",
                    ArticleType = ArticleType.Article,
                    CreatorName = "Admin",
                    Content = @"<p>Have you ever seen your cat licking their paws and wondered what the reason is for the peculiar habit ? It turns out that they don't just do it to be clean. As dedicated followers of a regular grooming routine, cats spend a lot of time making themselves look good, but paw licking is also tied to your kitty's physical and emotional well - being.</p>
<p></p>
<p> Why Do Cats Lick Their Paws ?</p>
<p> Paw licking is one way that cats clean themselves — distributing saliva all over their body helps them with grooming.And primping takes up a lot of a cat's time: ""Cats typically spend between 30 and 50 percent of their day grooming themselves, says Dr. Pamela Perry, a veterinarian and an animal behavior resident of the Animal Behavior Clinic at Cornell University's College of Veterinary Medicine.Because saliva helps cats cool off when they're overheated, licking their paws provides the added benefit of cooling relief in high temperatures.</p>
<p></p>
<p> According to the Cummings School of Veterinary Medicine at Tufts University, one other reason that cats lick themselves is that licking — and grooming in general — releases endorphins, the body's feel-good hormone. It's a calming activity.</p>
<p></p>
<p> Brown tabby cat with their eyes closed while licking its front paw </p>
   <p></p>
   <p> Excessive Paw Licking</p>
      <p> If your cat is paying too much attention to their paws, it likely indicates an underlying medical issue.Instead of trying to stop the habit itself, bring your kitty to the vet, so they can identify and treat the issue behind your cat's behavior.</p>
        <p></p>
        <p> There are several physical and psychological issues that can lead to excessive grooming, explains the Cummings School of Veterinary Medicine, including:</p>
           <p></p>
           <p> Allergies </p>
           <p> Fleas </p>
           <p> Dry skin </p>
              <p> A neurological condition</p>
                 <p> Stress or anxiety</p>
                    <p> Causes of cat anxiety include being separated from their pet parent; environmental changes, like moving into a new home; and perceived threats, such as having another pet in the household.</p>
                       <p></p>
                       <p> Diagnosis and Treatment</p>
                          <p> Is your cat licking their paws too much? If you think their behavior is excessive, keep a closer eye on them. Take note of when they lick their paws and how long they spend doing it.Note any changes to their skin or fur, such as irritation or hair loss.Additionally, check for any signs of pain in the paw.If you notice any of these changes make sure to bring them in to their veterinarian.This information will help your vet determine whether your kitty is licking their paws too frequently or too abrasively.</p>
                           <p></p>
                           <p> At the appointment, your vet will complete a physical exam of your cat.They'll probably run a few tests to determine the cause of and treatment for their behavior. Treatment will vary based on what your veterinarian diagnoses, but may include skin cream, oral or injected anti-inflammatory medicine, changes to your cat's food, flea and tick prevention medicine, pheromone therapy or environmental modifications, says Vetwest, adding that antidepressant or anti - anxiety medicine is also an option.</p>
                           <p></p>
                           <p> Environmental modifications, or enrichments, are opportunities for your cat to get more activity and stimulation within the house.This can be as simple as feeding them using a food puzzle, providing them more opportunities to utilize their climbing instincts by installing cat trees or shelves, and have them hunt for their toys.</ p >
                             <p></p>
                             <p> Your vet may also refer you to a cat behaviorist if they think that environmental or social modification could help, notes International Cat Care.</p>
                                <p></p>
                                <p> In general, your cat licking their paws isn't cause for concern. But if they show signs of excessive paw licking, speak with your vet as soon as possible. Together, you and your vet can determine the best treatment for your furry friend.</p>"
                },
                new Article
                {
                    Title = "Why Do Dogs Chase Cars? (& How to Get Them to Stop)",
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/11/19/14/07/animal-1839447_1280.jpg",
                    ArticleType = ArticleType.Article,
                    CreatorName = "Admin",
                    Content = @"<p>If your pet seems to be called to chase anything with wheels, you might be left wondering, ""Why do dogs chase cars?""</p>
<p></p>
<p>It's not like they can outrun them, and even if they could, how would they benefit from the end result? The behavior seems strange to say the least, but now you're curious. What causes a dog to chase cars? Let's take a closer look at what may be causing this behavior and how to stop a dog from chasing cars.</p>
<p></p>
<p>Why Do Dogs Chase Cars?</p>
<p>Although humans may not quite understand it, for dogs, chasing is an instinct. For dogs, moving vehicles may be an annoyance, a thrill or something else entirely, but one thing is for sure: It sparks that natural instinct in which a dog recognizes the vehicle as prey they must run after and capture.</p>
<p></p>
<p> And it's not just four-wheeled on-road vehicles, like cars or buses, that your dog might chase. There are other-wheeled vehicles that a dog might be just as motivated to follow, such as bikes, scooters or mopeds. Your dog may even chase people on Rollerblades or in wheelchairs!</p>
<p></p>
<p> Because chasing is a natural instinct, any type of dog breed may feel the drive to chase a car or other - wheeled form of transportation.However, the American Kennel Club(AKC) reports that sighthounds of all sizes and other herding breeds may be particularly driven to chase.</p>
<p></p>
<p> Black and white border collie running on the green grass </p>
<p></p>
<p> The Dangers of Chasing Cars </p>
<p> One of the most important things to keep in mind if your dog is chasing a moving vehicle on - or off - roads is that if they continue to chase, they might get hit.A collision could cause serious damage to your pet — damage that could potentially be life-threatening.If your dog is chasing and has problems with aggressive behavior, you also have to worry about your pet potentially attacking someone if they're able to catch up to them — like someone on Rollerblades who was simply skating by your property.</p>
    <p></p>
    <p> How to Stop a Dog from Chasing Cars</p>
       <p> The good news is that you can train your dog not to chase cars or other forms of transportation.However, for some particularly chase - driven pets, the training may prove difficult.The AKC reports, ""The desire to chase is inherent to many dogs and is a highly self-rewarding behavior ... Because some dogs enjoy it so much, it can be extra challenging to train them not to do it."" </p>
         <p></p>
         <p> Still, this doesn't mean you should give up hope. Here are a few tips to train your pet on impulse control:</p>
         <p></p>
         <p> Start training before the impulse strikes.It will be a lot harder to stop the behavior while it's happening than working in calm conditions first.</p>
           <p> Keep your dog on a leash and close to you during your training.</p>
              <p> Begin by teaching your dog the ""stay"" command.</p>
                 <p> When your dog understands the command, introduce scenarios that challenge their impulse control, such as a member of the family on a skateboard or slowly backing out of the driveway while your dog continues to stay seated or lying down in a still position.This phase of training will take the most time.Here, you'll need to increase speed or exposure, all while maintaining safety and keeping your dog leashed and close to you.</p>
                  <p> If at all possible, consider working directly with a local dog trainer for maximum results in the safest environment.</p>
                     <p></p>
                     <p> So, why do dogs chase cars ? The answer lies in natural evolution: They simply have the instinct to chase, and a quick - moving car appears just like their prey. Training your dog to stay immobile or by your side can help so that chasing cars becomes a thing of the past.</p>"
                },
                new Article
                {
                    Title = "Sailor: Deaf dog meets the perfect match",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/06/25/16/50/dog-2441258_1280.jpg",
                    ArticleType = ArticleType.Story,
                    CreatorName = "Volunteer",
                    Content = @"<p>When Allison Arnold and her partner, Lily, moved into their new home, they took one look at the big backyard and knew what they had to do: adopt another dog.So, they started looking online at available pups.That’s when they first learned about Sailor, who was in the process of amassing quite a fan following at Best Friends in Atlanta.</p>
<p></p>
<p> You see, there’s a lot to love about the young pit bull mix.He’s equal parts sweet and goofy, and so adorable. He’s also deaf, and that’s what really got Allison and Lily during the adoption process: Allison is a speech pathologist and Lily works as a special education teacher.</p>
<p></p>
<p>“We’ve always had a soft spot for animals, especially those that have been abandoned, have difficult circumstances or have special needs that require more care,” says Allison. “When we saw Sailor and learned he was deaf, we knew he was the one.”</p>
  <p></p>
  <p> When Sailor was at Best Friends, the staff and volunteers put together a plan to help him learn sign language(ASL) and his foster family continued to help teach him.Today, it’s a big part of how Allison and Lily communicate with him.</p>
  <p></p>
  <p> It’s not always easy, but the rewards certainly outweigh the challenges. “His deafness doesn’t hold him back from anything,” says Allison. “He just adapts and continues enjoying life.That's exactly what we should all be doing as well.”</p>"
                },
                new Article
                {
                    Title = "Romeo: The kitten who helped mend a broken heart",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/10/24/21/07/boy-2886169_1280.jpg",
                    ArticleType = ArticleType.Story,
                    CreatorName = "Volunteer",
                    Content = @"<p>Twelve-year-old Lucas Sweeney was absolutely heartbroken when his beloved cat, Very Good Kitten (VGK), passed away recently. VGK was adopted a few years ago by the family and quickly became Lucas’ shadow. When he came home from school, he hung out with VGK, and at night they slept together soundly. In fact, it was Lucas who gave VGK his nickname.</p>
<p></p>
<p>After VGK passed, Lucas’ mom, Laurie, decided the time was right to foster a cat, thinking it just might lift Lucas’ spirits. She signed the family up with Best Friends in Utah to become kitten fosters. Not too long after that, she hopped in the car with Lucas and picked up three kittens to foster, including a little black one named Romeo. “It was like he was sent from VGK,” says Laurie.</p>
<p></p>
<p>[Baby meets a kitten his age, cuteness ensues]</p>
<p></p>
<p>Today all three kittens fostered by the Sweeney family have been adopted. Romeo didn’t even have to leave the house to make that happen. He’s now Lucas’ new special friend, proving that fostering (and adopting) really can cure a broken heart.</p>"
                },
                new Article
                {
                    Title = "Dakota: Shy dog learns life is nothing to be afraid of",
                    ImageUrl = "https://cdn.pixabay.com/photo/2019/12/04/08/44/dog-4672117_1280.jpg",
                    ArticleType = ArticleType.Story,
                    CreatorName = "Volunteer",
                    Content = @"<p>For Dakota, new was always scary. When she was comfortable and with the people she loved, she was a snuggly couch potato. New faces, however, made her freeze up with nerves, unable to move until she felt safe again. It was no different when Michael and Alena Eberle met her for the first time.Dakota was nervous, and they wondered if she would be able to adjust well in their home. But they took a chance on her.</p>
<p></p>
<p>[Veterinarian is a senior dog’s prescription for the good life]</p>
 <p></p>
 <p> At first it was hard, they said.She couldn’t relax, and they had to wake her up in the middle of the night when she had nightmares.With time and patience, she finally started to open up. Once she knew that these were her people and she was safe, she even started getting excited about going to work with them and meeting all sorts of new people.With her family beside her, new wasn’t scary anymore.It was fun.</p>"
                },
                new Article
                {
                    Title = "Steak: Big orange tabby is living large in the Lone Star State",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/03/27/12/44/man-2178466_1280.jpg",
                    ArticleType = ArticleType.Story,
                    CreatorName = "Volunteer",
                    Content = @"<p>Steak has a way of stealing the spotlight like only a big orange tabby cat can. That’s what Diane Barber discovered when she stepped up to foster him during the pandemic. He became an instant hit during virtual happy hours with Diane’s friends and reigned supreme over her Facebook page.</p>
<p></p>
<p>That’s where Steak, who came to Best Friends in Los Angeles from an L.A.city shelter, caught the attention of Diane’s friend, Doug Parker.He got to thinking that he might like to adopt Steak, but there was just one logistical conundrum: Doug lives in Houston.How in the world could Steak get from California to Texas?</p>
<p></p>
<p>Diane had the answer: She and her partner would drive Steak to his new home with Doug.And as soon as the stay-at-home order was lifted in L.A., they hit the road with Steak in tow.</p>
<p></p>
<p>Turns out, the orange tabby is quite the road tripper, sitting patiently in the back seat and making himself at home in hotels like a seasoned traveler.The best part: Steak also quickly made himself at home at Doug’s place.Recently, Diane visited Steak and reports he’s happy and living large in the Lone Star State.</p>"
                },
                new Article
                {
                    Title = "Monkey: 30 pounds of irresistiblee",
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/02/12/20/15/lucky-cat-1196761_1280.jpg",
                    ArticleType = ArticleType.Story,
                    CreatorName = "Volunteer",
                    Content = @"<p>Monkey is a cat so cute, it’s like he’s got his own gravity. When the 31-pound cat first arrived at Best Friends Animal Sanctuary, everyone was drawn to him and his outgoing personality. And he loved the attention. It wasn’t just people who met him in person, either. When Jennifer May saw his picture on the Best Friends website, she saw his big green eyes and was pulled in like everyone else before traveling more than 300 miles to pick him up.</p >
<p></p>
<p> The happy ending didn’t stop there, though.When Jennifer came to get Monkey, she fell in love with two more cats ― brothers Jupiter and Saturn. And, so, Monkey’s gravity of cuteness pulled two little planets all the way home with him.</p>"
                },
            });

            data.SaveChanges();
        }

        private static void SeedEvents(HighPawDbContext data)
        {
            data.Events.AddRange(new List<Event>
            {
                new Event
                {
                    Title = "Animal Care and Control Appreciation Week",
                    Description = @"The National Animal Care & Control Association is proud to endorse our annual effort to celebrate and promote professionalism within the Animal Care and Control field on the national stage. As with other events designed to promote specific groups, professions, and other important causes, NACA is pleased to provide all the necessary encouragement for all localities who would like to show their appreciation to all their Animal Care and Control personnel. We encourage all Animal Care and Control agencies to have a special week of their own to show off their pride and receive recognition for the important services they provide to their communities.

This week of appreciation is designed to give recognition to the hard-working men and women of Animal Care and Control who risk their lives and devote huge amounts of personal time and resources, while they serve the public like other public safety and law enforcement agencies empowered with the same duties.

This is the week that these hard working and dedicated Animal Care and Control employees should be honored by having the whole community say, “Thank You”, for helping when no one else could, or would even know how to.",
                    Location = "Four Paws",
                    Date = DateTime.UtcNow.AddDays(3)
                },
                new Event
                {
                    Title = "Animal Care Expo 2022 - HSUS",
                    Description = @"Animal Care Expo is the largest international educational conference and trade show for animal welfare professionals and volunteers. Over 1,500 attendees are expected to join HSUS (Humane Society of the United States) in Orlando in 2022. Experts from all aspects of animal welfare will come together from across the globe to learn about the latest programs, share best practices, gain inspiration and build lasting connections. The conference features an international trade show promoting the latest animal care products and services from a wide range of exhibitors. With eleven workshop tracks, learning labs, networking opportunities and social events, Animal Care Expo has something for everyone who cares about companion animals.

This year will be the first-ever hybrid conference experience April 19-22, 2022, in Orlando, Florida, at the Orlando World Center Marriott. Animal Care Expo 2022 will feature over 80 professional development sessions in-person and select live-streamed sessions for a virtual audience. These broadcasted sessions will be recorded and available to both in-person and virtual participants for on-demand viewing after the conference.",
                    Location = "Four Paws",
                    Date = DateTime.UtcNow.AddDays(7)
                },
                new Event
                {
                    Title = "Lifesaving impact week",
                    Description = @"Lifesaving Impact Week will feature Petco Love's Lifesaving Awards. The Lifesaving Awards are a celebration of love that saves animal lives and represents our thanks to those people who put their love into action every day. The awards recognize exemplary performance by organizations and individuals whose dedica",
                    Location = "OPBK",
                    Date = DateTime.UtcNow.AddDays(10)
                },
                new Event
                {
                    Title = "Helping with food and supplies",
                    Description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Location = "OPBK",
                    Date = DateTime.UtcNow.AddDays(12)
                },
                new Event
                {
                    Title = "Be our hero",
                    Description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    Location = "Animal Hope Varna",
                    Date = DateTime.UtcNow.AddDays(14)
                }
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdminRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@admin.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedVolunteer(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = VolunteerRoleName };

                    await roleManager.CreateAsync(role);

                    const string volunteerEmail = "test@volunteer.com";
                    const string volunteerPassword = "vtest123";

                    var user = new User
                    {
                        Email = volunteerEmail,
                        UserName = VolunteerRoleName,
                        FullName = "User Volunteer"
                    };

                    await userManager.CreateAsync(user, volunteerPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}