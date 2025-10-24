using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetById;

public record AppUserGetByIdQuery(Guid Id) : IRequest<BaseResult<AppUserGetByIdQueryResponseDto>>;