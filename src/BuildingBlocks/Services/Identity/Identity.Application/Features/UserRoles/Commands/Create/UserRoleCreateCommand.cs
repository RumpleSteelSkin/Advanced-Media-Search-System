using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transactional;
using MediatR;

namespace Identity.Application.Features.UserRoles.Commands.Create;

public record UserRoleCreateCommand(Guid UserId, Guid RoleId)
    : IRequest<BaseResult<object>>, IRoleExists, ITransactional
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}