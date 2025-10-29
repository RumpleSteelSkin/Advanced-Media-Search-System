using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetAll;

public record RoleGetAllQuery : IRequest<BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}