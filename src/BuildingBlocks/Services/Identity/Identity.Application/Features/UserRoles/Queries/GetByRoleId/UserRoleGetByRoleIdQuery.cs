using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Authorization;
using Identity.Application.Constant;
using MediatR;

namespace Identity.Application.Features.UserRoles.Queries.GetByRoleId;

public record UserRoleGetByRoleIdQuery(Guid RoleId) : IRequest<BaseResult<UserRoleGetByRoleIdQueryResponseDto>>,IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}