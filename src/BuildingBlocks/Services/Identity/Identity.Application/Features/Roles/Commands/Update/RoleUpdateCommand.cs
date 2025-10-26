using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transactional;
using Identity.Application.Constant;
using MediatR;

namespace Identity.Application.Features.Roles.Commands.Update;

public record RoleUpdateCommand(Guid Id, string Name) : IRequest<BaseResult<object>>, IRoleExists, ITransactional
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}