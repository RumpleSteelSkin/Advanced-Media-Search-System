using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Roles.Commands.Create;

public record RoleCreateCommand(string Name) : IRequest<BaseResult<object>>;