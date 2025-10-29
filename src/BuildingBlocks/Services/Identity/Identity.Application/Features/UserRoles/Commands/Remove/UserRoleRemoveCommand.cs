using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.UserRoles.Commands.Remove;

public record UserRoleRemoveCommand(Guid UserId, Guid RoleId) : IRequest<BaseResult<object>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}