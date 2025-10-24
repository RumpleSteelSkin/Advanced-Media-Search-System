using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Roles.Commands.Create;

public class RoleCreateCommandHandler(RoleManager<AppRole> roleManager)
    : IRequestHandler<RoleCreateCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
    {
        var response = await roleManager.CreateAsync(new AppRole { Name = request.Name });
        return response.Succeeded ? BaseResult<object>.Success(response) : BaseResult<object>.Fail(response.Errors);
    }
}