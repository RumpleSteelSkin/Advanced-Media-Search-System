using Core.Application.Base.BaseResult;
using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transactional;
using MediatR;

namespace Identity.Application.Features.Users.Commands.Update;

public record AppUserUpdateCommand : IRequest<BaseResult<object>>, ITransactional, IRoleExists
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string[] Roles { get; } = [GeneralRoles.Admin, GeneralRoles.User];
}