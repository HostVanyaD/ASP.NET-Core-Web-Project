namespace HighPaw.Web.Infrastructure
{
    using AutoMapper;
    using HighPaw.Data.Models;
    using HighPaw.Services.Pet.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //this.CreateMap<Category, CarCategoryServiceModel>();

            this.CreateMap<Pet, LatestPetServiceModel>()
                .ForMember(p => p.ShelterName, cfg => cfg.MapFrom(p => p.Shelter.Address));

            //this.CreateMap<CarDetailsServiceModel, CarFormModel>();

            //this.CreateMap<Car, CarServiceModel>()
            //    .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));

            //this.CreateMap<Car, CarDetailsServiceModel>()
            //    .ForMember(c => c.UserId, cfg => cfg.MapFrom(c => c.Dealer.UserId))
            //    .ForMember(c => c.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name));
        }
    }
}
