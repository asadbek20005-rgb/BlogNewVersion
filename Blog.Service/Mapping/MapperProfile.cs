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
    }
}