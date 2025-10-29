using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetAll;

public record AppUserGetAllQuery : IRequest<BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}