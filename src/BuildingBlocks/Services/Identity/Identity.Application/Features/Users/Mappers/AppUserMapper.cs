using AutoMapper;
using Identity.Application.Features.Users.Commands.Create;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Users.Mappers;

public class AppUserMapper : Profile
{
    public AppUserMapper()
    {
        CreateMap<AppUserCreateCommand, AppUser>();
    }
}