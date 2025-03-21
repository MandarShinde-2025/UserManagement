using AutoMapper;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappings;

public class UsersMappingProfile: Profile
{
    public UsersMappingProfile()
    {
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}