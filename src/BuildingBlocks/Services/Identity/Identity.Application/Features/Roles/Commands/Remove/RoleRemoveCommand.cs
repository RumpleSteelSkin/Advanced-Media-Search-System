using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Roles.Commands.Remove;

public record RoleRemoveCommand(Guid Id) : IRequest<BaseResult<object>>;