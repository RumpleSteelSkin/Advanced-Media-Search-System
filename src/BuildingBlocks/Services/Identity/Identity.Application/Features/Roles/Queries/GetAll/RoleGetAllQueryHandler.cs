using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Roles.Queries.GetAll;

public class RoleGetAllQueryHandler(IMapper mapper, RoleManager<AppRole> roleManager)
    : IRequestHandler<RoleGetAllQuery, BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>>
{
    public async Task<BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>> Handle(RoleGetAllQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.ToListAsync(cancellationToken);
        if (roles.Count == 0) return BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>.NotFound("Role not found");
        var response = mapper.Map<IEnumerable<RoleGetAllQueryResponseDto>>(roles);
        return BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>.Success(response);
    }
}