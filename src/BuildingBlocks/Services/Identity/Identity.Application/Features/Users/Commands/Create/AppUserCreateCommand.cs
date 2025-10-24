using Core.Application.Base.BaseResult;
using Core.Application.Pipelines.Transactional;
using MediatR;

namespace Identity.Application.Features.Users.Commands.Create;

public record AppUserCreateCommand : IRequest<BaseResult<object>>, ITransactional
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}