using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Dtos;
using EFCore.Models.Dtos.Auth;
using EFCore.MySQL.Models.Dto;
using Web.Api.Constants;

namespace EFCore.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Computer, ComputerDto>()
                .ReverseMap();
            CreateMap<AppUser, CustomerDto>()
                .ReverseMap();
            CreateMap<AppUser, CustomerWithSubscriptionsDto>()
                .ReverseMap();
            CreateMap<Subscription, SubscriptionDto>()
                .ReverseMap();
            CreateMap<AppUser, RegisterDto>()
               .ReverseMap();
            
            CreateMap<AppUser, DefaultSuperAdmin> ()
               .ReverseMap();
             CreateMap<AppUser, DefaultAdmin> ()
               .ReverseMap();
             CreateMap<AppUser, DefaultCustomer> ()
               .ReverseMap();

        }
    }
}
