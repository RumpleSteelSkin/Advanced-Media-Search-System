using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Authorization;
using Identity.Application.Constant;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetById;

public record AppUserGetByIdQuery(Guid Id) : IRequest<BaseResult<AppUserGetByIdQueryResponseDto>>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin, GeneralRoles.User];
}