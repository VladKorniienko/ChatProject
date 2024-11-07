using AutoMapper;
using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.AutoMapperProfiles
{
    public class UserToUserDtoProfile : Profile
    {
        public UserToUserDtoProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
