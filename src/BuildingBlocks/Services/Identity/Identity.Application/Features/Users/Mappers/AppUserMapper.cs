using AutoMapper;
using Identity.Application.Features.Shared.DTOs;
using Identity.Application.Features.Users.Commands.Create;
using Identity.Application.Features.Users.Commands.Update;
using Identity.Application.Features.Users.Queries.GetAll;
using Identity.Application.Features.Users.Queries.GetById;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Users.Mappers;

public class AppUserMapper : Profile
{
    public AppUserMapper()
    {
        CreateMap<AppUserCreateCommand, AppUser>();
        CreateMap<AppUser, AppUserGetByIdQueryResponseDto>();
        CreateMap<AppUser, UserDto>();
        CreateMap<AppUserUpdateCommand, AppUser>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<AppUser, AppUserGetAllQueryResponseDto>();
    }
}