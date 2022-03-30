namespace HighPaw.Web.Infrastructure
{
    using AutoMapper;
    using HighPaw.Data.Models;
    using HighPaw.Data.Models.Enums;
    using HighPaw.Services.Admin.Models;
    using HighPaw.Services.Pet.Models;
    using HighPaw.Services.Shelter.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<SizeCategory, SizeCategoryServiceModel>()
                .ReverseMap();

            this.CreateMap<Shelter, ShelterNameServiceModel>()
                .ReverseMap();

            this.CreateMap<Shelter, ShelterServiceModel>()
                .ReverseMap();

            this.CreateMap<Pet, PetListingServiceModel>()
                .ForMember(p => p.ShelterName, cfg => cfg.MapFrom(p => p.Shelter.Address));

            this.CreateMap<Pet, PetDetailsServiceModel>()
                .ForMember(pd => pd.PetType, cfg => cfg.MapFrom(p => p.PetType == PetType.Dog ? "Dog" : "Cat"))
                .ForMember(pd => pd.MicrochipId, cfg => cfg.MapFrom(p => p.MicrochipId ?? "Not info available"))
                .ForMember(pd => pd.Shelter, cfg => cfg.MapFrom(p => p.Shelter));

            this.CreateMap<Pet, AdminPetListingServiceModel>()
                .ForMember(ap => ap.Type, cfg => cfg.MapFrom(p => p.PetType == PetType.Dog ? "Dog" : "Cat"))
                .ForMember(ap => ap.LostOrFound, cfg => cfg.MapFrom(p => p.IsLost == true ? "Lost" : "Found"));

            this.CreateMap<Article, AdminArticleListingServiceModel>()
                .ReverseMap();

            //this.CreateMap<CarDetailsServiceModel, CarFormModel>();

            //this.CreateMap<Car, CarServiceModel>()
            //    .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));

            //this.CreateMap<Car, CarDetailsServiceModel>()
            //    .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId))
            //    .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
