using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Application.Features.Shared.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.UserRoles.Queries.GetByRoleId;

public class UserRoleGetByRoleIdQueryHandler(
    IMapper mapper,
    DbContext context)
    : IRequestHandler<UserRoleGetByRoleIdQuery, BaseResult<UserRoleGetByRoleIdQueryResponseDto>>
{
    public async Task<BaseResult<UserRoleGetByRoleIdQueryResponseDto>> Handle(
        UserRoleGetByRoleIdQuery request,
        CancellationToken cancellationToken)
    {
        var roleUsers = await (from ur in context.Set<IdentityUserRole<Guid>>()
                join u in context.Set<AppUser>() on ur.UserId equals u.Id
                join r in context.Set<AppRole>() on ur.RoleId equals r.Id
                where !u.IsDeleted && !r.IsDeleted && ur.RoleId == request.RoleId
                select new { u, r })
            .ToListAsync(cancellationToken);

        if (roleUsers.Count == 0)
            return BaseResult<UserRoleGetByRoleIdQueryResponseDto>.NotFound("No users for this role");

        var role = mapper.Map<RoleDto>(roleUsers.First().r);
        var users = roleUsers.Select(x => mapper.Map<UserDto>(x.u)).ToList();

        return BaseResult<UserRoleGetByRoleIdQueryResponseDto>.Success(new UserRoleGetByRoleIdQueryResponseDto
        {
            Role = role,
            Users = users
        });
    }
}