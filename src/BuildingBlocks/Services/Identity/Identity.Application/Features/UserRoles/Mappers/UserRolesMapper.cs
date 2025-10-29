using AutoMapper;
using Identity.Application.Features.Shared.DTOs;
using Identity.Domain.Entities;

namespace Identity.Application.Features.UserRoles.Mappers;

public class UserRolesMapper : Profile
{
    public UserRolesMapper()
    {
        CreateMap<AppRole, RoleDto>();
        CreateMap<AppUser, UserDto>();
    }
}