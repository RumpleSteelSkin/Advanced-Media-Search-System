using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.UserRoles.Queries.GetAll;

public record UserRoleGetAllQuery : IRequest<BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}