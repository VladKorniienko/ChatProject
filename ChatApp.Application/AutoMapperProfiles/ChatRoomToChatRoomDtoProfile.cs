using AutoMapper;
using ChatApp.Application.Dtos;
using ChatApp.Domain.Entities;

namespace ChatApp.Application.AutoMapperProfiles
{
    public class ChatRoomToChatRoomDtoProfile : Profile
    {
        public ChatRoomToChatRoomDtoProfile()
        {
            CreateMap<ChatRoom, ChatRoomDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<User, UserDto>();
            CreateMap<Message, MessageDto>();
        }
    }
}
