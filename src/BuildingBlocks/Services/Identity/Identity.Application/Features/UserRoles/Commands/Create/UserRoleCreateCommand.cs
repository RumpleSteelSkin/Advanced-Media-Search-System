using Core.Application.Base.BaseResult;
using Identity.Application.Features.UserRoles.Commands.Create;
using MediatR;

namespace Identity.Application.Features.UserRoles.Commands.Create;

public record UserRoleCreateCommand(Guid UserId, Guid RoleId) : IRequest<BaseResult<object>>;