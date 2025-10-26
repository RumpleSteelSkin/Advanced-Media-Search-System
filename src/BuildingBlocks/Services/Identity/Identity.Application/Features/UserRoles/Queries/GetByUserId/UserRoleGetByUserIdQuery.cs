using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Authorization;
using Identity.Application.Constant;
using MediatR;

namespace Identity.Application.Features.UserRoles.Queries.GetByUserId;

public record UserRoleGetByUserIdQuery(Guid Id) : IRequest<BaseResult<UserRoleGetByUserIdQueryResponseDto>>,IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}