using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Roles.Queries.GetById;

public class RoleGetByIdQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper)
    : IRequestHandler<RoleGetByIdQuery, BaseResult<RoleGetByIdQueryResponseDto>>
{
    public async Task<BaseResult<RoleGetByIdQueryResponseDto>> Handle(RoleGetByIdQuery request,
        CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());
        if (role is null) return BaseResult<RoleGetByIdQueryResponseDto>.Fail("Role not found");
        var response = mapper.Map<RoleGetByIdQueryResponseDto>(role);
        return BaseResult<RoleGetByIdQueryResponseDto>.Success(response);
    }
}