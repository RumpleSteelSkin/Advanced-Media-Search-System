using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transactional;
using MediatR;

namespace Identity.Application.Features.UserRoles.Commands.Update;

public record UserRoleUpdateCommand(Guid UserId, Guid ExistRoleId, Guid NewRoleId)
    : IRequest<BaseResult<object>>, ITransactional, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}