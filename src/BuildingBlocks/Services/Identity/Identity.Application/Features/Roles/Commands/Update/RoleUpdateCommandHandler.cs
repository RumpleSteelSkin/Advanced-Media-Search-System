using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Roles.Commands.Update;

public class RoleUpdateCommandHandler(RoleManager<AppRole> roleManager)
    : IRequestHandler<RoleUpdateCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(RoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var exist = await roleManager.FindByIdAsync(request.Id.ToString());
        if (exist is null)
            return BaseResult<object>.NotFound("Role not found");
        
        var nameResult = await roleManager.SetRoleNameAsync(exist, request.Name);
        if (!nameResult.Succeeded)
            return BaseResult<object>.Fail("Role name could not be set");
        
        var updateResult = await roleManager.UpdateAsync(exist);
        return updateResult.Succeeded
            ? BaseResult<object>.Success("Role updated successfully")
            : BaseResult<object>.Fail("An error occurred while updating role");
    }
}
