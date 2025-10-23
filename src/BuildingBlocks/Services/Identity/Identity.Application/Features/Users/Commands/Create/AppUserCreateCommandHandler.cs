using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Commands.Create;

public class AppUserCreateCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
    : IRequestHandler<AppUserCreateCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(AppUserCreateCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<AppUser>(request);
        var result = await userManager.CreateAsync(user, request.Password ?? string.Empty);
        return result.Succeeded
            ? BaseResult<object>.Success(new { user.Id, user.UserName, user.Email })
            : BaseResult<object>.Fail(result.Errors.Select(e => e.Description));
    }
}