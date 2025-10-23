using Core.Application.Base.BaseResult;
using MediatR;

namespace Identity.Application.Features.Users.Commands.Create;

public class AppUserCreateCommand : IRequest<BaseResult<object>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}