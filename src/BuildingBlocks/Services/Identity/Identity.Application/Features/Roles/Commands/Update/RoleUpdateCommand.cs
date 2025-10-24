using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Roles.Commands.Update;

public record RoleUpdateCommand(Guid Id, string Name) : IRequest<BaseResult<object>>;