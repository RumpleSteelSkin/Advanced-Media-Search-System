using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.UserRoles.Queries.GetByRoleId;

public record UserRoleGetByRoleIdQuery(Guid RoleId)
    : IRequest<BaseResult<UserRoleGetByRoleIdQueryResponseDto>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}