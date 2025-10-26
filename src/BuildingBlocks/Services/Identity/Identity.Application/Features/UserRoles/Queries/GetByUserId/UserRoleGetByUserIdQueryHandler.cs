using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Application.Features.Shared.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.UserRoles.Queries.GetByUserId;

public class UserRoleGetByUserIdQueryHandler(
    IMapper mapper,
    DbContext context)
    : IRequestHandler<UserRoleGetByUserIdQuery, BaseResult<UserRoleGetByUserIdQueryResponseDto>>
{
    public async Task<BaseResult<UserRoleGetByUserIdQueryResponseDto>> Handle(
        UserRoleGetByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var userRoles = await (from ur in context.Set<IdentityUserRole<Guid>>()
                join u in context.Set<AppUser>() on ur.UserId equals u.Id
                join r in context.Set<AppRole>() on ur.RoleId equals r.Id
                where !u.IsDeleted && !r.IsDeleted && ur.UserId == request.Id
                select new { u, r })
            .ToListAsync(cancellationToken);

        if (userRoles.Count == 0)
            return BaseResult<UserRoleGetByUserIdQueryResponseDto>.NotFound("No user or roles found");

        var user = mapper.Map<UserDto>(userRoles.First().u);
        var roles = userRoles.Select(x => mapper.Map<RoleDto>(x.r)).ToList();

        return BaseResult<UserRoleGetByUserIdQueryResponseDto>.Success(new UserRoleGetByUserIdQueryResponseDto
        {
            User = user,
            Roles = roles
        });
    }
}