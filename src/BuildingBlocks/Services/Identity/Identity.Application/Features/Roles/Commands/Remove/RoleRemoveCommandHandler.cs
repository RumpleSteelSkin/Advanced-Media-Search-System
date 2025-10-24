using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Roles.Commands.Remove;

public class RoleRemoveCommandHandler(RoleManager<AppRole> roleManager)
    : IRequestHandler<RoleRemoveCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(RoleRemoveCommand request, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(request.Id.ToString());
        if (role is null) return BaseResult<object>.NotFound("Role not found");
        role.DeletedAt = DateTime.UtcNow;
        role.IsDeleted = true;
        var response = await roleManager.UpdateAsync(role);
        return response.Succeeded
            ? BaseResult<object>.Success("Role deleted")
            : BaseResult<object>.Fail("An error occured");
    }
}