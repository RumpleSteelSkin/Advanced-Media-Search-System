using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Identity.Application.Features.UserRoles.Queries.GetByUserId;

public record UserRoleGetByUserIdQuery(Guid Id) : IRequest<BaseResult<UserRoleGetByUserIdQueryResponseDto>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}