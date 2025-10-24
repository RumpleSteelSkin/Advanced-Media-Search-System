using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.UserRoles.Commands.Create;

public class UserRoleCreateCommandHandler(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    : IRequestHandler<UserRoleCreateCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UserRoleCreateCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role is null) return BaseResult<object>.NotFound("Role not found");
        if (role.IsDeleted) return BaseResult<object>.Fail($"{role.Name} role deleted. DeletedAt: {role.DeletedAt}");

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null) return BaseResult<object>.NotFound("User not found");
        if (user.IsDeleted)
            return BaseResult<object>.Fail($"{user.UserName} user deleted.  DeletedAt: {user.DeletedAt}");

        var response = await userManager.AddToRoleAsync(user, role.Name!);
        return response.Succeeded
            ? BaseResult<object>.Success($"{role.Name} role granted to {user.UserName}")
            : BaseResult<object>.Fail("Role not granted");
    }
}