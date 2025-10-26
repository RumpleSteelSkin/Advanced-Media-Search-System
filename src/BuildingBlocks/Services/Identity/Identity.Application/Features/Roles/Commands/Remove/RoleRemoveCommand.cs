using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transactional;
using Identity.Application.Constant;
using MediatR;

namespace Identity.Application.Features.Roles.Commands.Remove;

public record RoleRemoveCommand(Guid Id) : IRequest<BaseResult<object>>,ITransactional,IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}