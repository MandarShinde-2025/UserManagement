using AutoMapper;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappings;

public class MenuItemMappingProfile : Profile
{
    public MenuItemMappingProfile()
    {
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
    }
}
