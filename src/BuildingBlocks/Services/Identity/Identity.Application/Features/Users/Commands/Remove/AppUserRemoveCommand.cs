using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Transactional;
using MediatR;

namespace Identity.Application.Features.Users.Commands.Remove;

public record AppUserRemoveCommand(Guid Id) : IRequest<BaseResult<object>>, ITransactional;