using AutoMapper;
using Core.Application.Base.BaseResult;
using Identity.Application.Contracts;
using Identity.Application.Features.Shared.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Queries.Login;

public class AppUserLoginQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IJwtService jwtService)
    : IRequestHandler<AppUserLoginQuery, BaseResult<AppUserLoginQueryResponseDto>>
{
    public async Task<BaseResult<AppUserLoginQueryResponseDto>> Handle(AppUserLoginQuery request,
        CancellationToken cancellationToken)
    {
        var usernameOrEmail = request.UserNameOrEmail.Trim();
        var user = usernameOrEmail.Contains('@')
            ? await userManager.FindByEmailAsync(usernameOrEmail)
            : await userManager.FindByNameAsync(usernameOrEmail);
        if (user is null) return BaseResult<AppUserLoginQueryResponseDto>.Fail("Invalid credentials");
        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if (!result) return BaseResult<AppUserLoginQueryResponseDto>.Fail("Invalid credentials");
        var userResult = mapper.Map<UserDto>(user);
        var response = await jwtService.GenerateTokenAsync(userResult);
        return response != null
            ? BaseResult<AppUserLoginQueryResponseDto>.Success(response)
            : BaseResult<AppUserLoginQueryResponseDto>.Fail("Token could not be generated");
    }
}