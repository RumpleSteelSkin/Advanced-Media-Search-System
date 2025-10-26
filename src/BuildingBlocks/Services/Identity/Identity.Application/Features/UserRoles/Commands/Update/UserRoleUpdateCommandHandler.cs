using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.UserRoles.Commands.Update;

public class UserRoleUpdateCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    : IRequestHandler<UserRoleUpdateCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UserRoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return BaseResult<object>.NotFound("User not found");

        var role = await roleManager.FindByIdAsync(request.ExistRoleId.ToString());
        if (role == null) return BaseResult<object>.NotFound("Role not found");

        var newRole = await roleManager.FindByIdAsync(request.NewRoleId.ToString());
        if (newRole == null) return BaseResult<object>.NotFound("New role not found");

        var removedRoleResult = await userManager.RemoveFromRoleAsync(user, role.Name ?? string.Empty);
        if (!removedRoleResult.Succeeded) return BaseResult<object>.NotFound("Existed role not found");

        var addResult = await userManager.AddToRoleAsync(user, newRole.Name ?? string.Empty);
        if (addResult.Succeeded) return BaseResult<object>.Success(addResult);
        await userManager.AddToRoleAsync(user, role.Name ?? string.Empty);
        return BaseResult<object>.Fail("This role already granted.");
    }
}