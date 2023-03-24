using AutoMapper;
using Common.DTOs;
using Common.DTOs.Subscription;
using Core.Domain.Entities;


namespace Users.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserCreateDTO>();
            CreateMap<ReadUserDTO, User>().ReverseMap();
            CreateMap<UpdateUserDTO, User>().ReverseMap();

            //Subscription
            CreateMap<Subscription, ReadSubscriptionDTO>().ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
        }
    }
}
