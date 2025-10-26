using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.UserRoles.Commands.Remove;

public class UserRoleRemoveCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    : IRequestHandler<UserRoleRemoveCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UserRoleRemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return BaseResult<object>.NotFound("User not found");
        var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null) return BaseResult<object>.NotFound("Role not found");
        var response = await userManager.RemoveFromRoleAsync(user, role.Name!);
        return response.Succeeded
            ? BaseResult<object>.Success(response)
            : BaseResult<object>.Fail("Role removal failed");
    }
}