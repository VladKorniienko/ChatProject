using AutoMapper;
using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.AutoMapperProfiles
{
    public class MessageToMessageDtoProfile : Profile
    {
        public MessageToMessageDtoProfile()
        {
            CreateMap<Message,MessageDto>().ReverseMap();
        }
    }
}
