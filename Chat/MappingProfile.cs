using AutoMapper;
using Chat.DTOs;
using Chat.Entities;

namespace Chat
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<User, UserForReturnDto>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            CreateMap<Message, MessageForSendDto>().ReverseMap();


        }
    }
}
