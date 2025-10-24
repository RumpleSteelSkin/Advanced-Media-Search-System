using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetById;

public record RoleGetByIdQuery(Guid Id) : IRequest<BaseResult<RoleGetByIdQueryResponseDto>>;