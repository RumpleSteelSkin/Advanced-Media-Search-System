using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetAll;

public record AppUserGetAllQuery : IRequest<BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>>;