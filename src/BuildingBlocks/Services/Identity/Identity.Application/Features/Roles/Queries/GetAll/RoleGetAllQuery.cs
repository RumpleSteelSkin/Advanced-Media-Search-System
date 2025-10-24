using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetAll;

public record RoleGetAllQuery : IRequest<BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>>;