using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Application.Features.Shared.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.UserRoles.Queries.GetAll;

public class UserRoleGetAllQueryHandler(
    IMapper mapper,
    DbContext context)
    : IRequestHandler<UserRoleGetAllQuery, BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>>
{
    public async Task<BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>> Handle(
        UserRoleGetAllQuery request,
        CancellationToken cancellationToken)
    {
        var query = from ur in context.Set<IdentityUserRole<Guid>>()
            join u in context.Set<AppUser>() on ur.UserId equals u.Id
            join r in context.Set<AppRole>() on ur.RoleId equals r.Id
            where !u.IsDeleted && !r.IsDeleted
            select new { u, r };
        
        var groupedDto = await query
            .GroupBy(x => x.u)
            .Select(g => new UserRoleGetAllQueryResponseDto
            {
                User = mapper.Map<UserDto>(g.Key),
                Roles = g.Select(x => mapper.Map<RoleDto>(x.r)).ToList()
            })
            .ToListAsync(cancellationToken);

        return groupedDto.Count == 0
            ? BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>.NotFound("No users with roles found")
            : BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>.Success(groupedDto);
    }
}