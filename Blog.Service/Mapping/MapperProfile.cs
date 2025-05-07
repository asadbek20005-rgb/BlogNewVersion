using AutoMapper;
using Blog.Common.Dtos;
using Blog.Common.Models.Otp;
using Blog.Common.Models.User;
using Blog.Data.Entities;

namespace Blog.Service.Mapster;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<RegisterModel, User>();
        CreateMap<OtpModel, Otp>();
        CreateMap<UpdateProfileModel, User>()
            .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName is not null))
            .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName is not null))
            .ForMember(dest => dest.DateOfBirth, opt => opt.Condition(src => src.DateOfBirth is not null))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email is not null))
            .ForMember(dest => dest.Bio, opt => opt.Condition(src => src.Bio is not null))
            .ForMember(dest => dest.GenderId, opt => opt.Condition(src => src.GenderId is not null));
    }
}