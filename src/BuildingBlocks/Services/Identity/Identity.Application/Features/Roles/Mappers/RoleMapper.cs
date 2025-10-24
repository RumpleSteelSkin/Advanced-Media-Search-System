using AutoMapper;
using Identity.Application.Features.Roles.Queries.GetAll;
using Identity.Application.Features.Roles.Queries.GetById;
using Identity.Domain.Entities;

namespace Identity.Application.Features.Roles.Mappers;

public class RoleMapper : Profile
{
    public RoleMapper()
    {
        CreateMap<AppRole, RoleGetAllQueryResponseDto>();
        CreateMap<AppRole, RoleGetByIdQueryResponseDto>();
    }
}