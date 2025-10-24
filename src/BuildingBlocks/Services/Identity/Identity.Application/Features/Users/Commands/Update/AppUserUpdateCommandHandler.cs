using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Commands.Update;

public class AppUserUpdateCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
    : IRequestHandler<AppUserUpdateCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(AppUserUpdateCommand request, CancellationToken cancellationToken)
    {
        var existUser = await userManager.FindByIdAsync(request.Id.ToString());
        if (existUser is null)
            return BaseResult<object>.NotFound("User not found");

        mapper.Map(request, existUser);

        if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != existUser.Email)
            await userManager.SetEmailAsync(existUser, request.Email);

        if (!string.IsNullOrWhiteSpace(request.UserName) && request.UserName != existUser.UserName)
            await userManager.SetUserNameAsync(existUser, request.UserName);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(existUser);
            var pwdResult = await userManager.ResetPasswordAsync(existUser, token, request.Password);
            if (!pwdResult.Succeeded)
                return BaseResult<object>.Fail("Password update failed");
        }

        var response = await userManager.UpdateAsync(existUser);
        return response.Succeeded
            ? BaseResult<object>.Success("User successfully updated")
            : BaseResult<object>.Fail("Failed to update user");
    }
}