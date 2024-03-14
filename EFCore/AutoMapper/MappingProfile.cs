using AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Dtos;
using EFCore.MySQL.Models.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFCore.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Computer, ComputerDto>()
                .ReverseMap();
            CreateMap<Customer, CustomerDto>()
                .ReverseMap();
            CreateMap<Customer, CustomerWithSubscriptionsDto>()
                .ReverseMap();
            CreateMap<Subscription, SubscriptionDto>()
                .ReverseMap();

        }
    }
}
