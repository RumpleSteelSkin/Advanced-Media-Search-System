using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Commands.Remove;

public class AppUserRemoveCommandHandler(UserManager<AppUser> userManager)
    : IRequestHandler<AppUserRemoveCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(AppUserRemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user is null) return BaseResult<object>.NotFound("User not found");
        if (user.IsDeleted) return BaseResult<object>.Fail($"The user has already been deleted on {user.DeletedAt?.ToShortDateString()}");
        user.DeletedAt = DateTime.UtcNow;
        user.IsDeleted = true;
        user.LockoutEnabled = true;
        await userManager.UpdateAsync(user);
        return BaseResult<object>.Success("User removed");
    }
}