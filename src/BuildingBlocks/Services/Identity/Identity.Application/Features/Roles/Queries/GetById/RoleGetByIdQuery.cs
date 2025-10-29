using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetById;

public record RoleGetByIdQuery(Guid Id) : IRequest<BaseResult<RoleGetByIdQueryResponseDto>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}