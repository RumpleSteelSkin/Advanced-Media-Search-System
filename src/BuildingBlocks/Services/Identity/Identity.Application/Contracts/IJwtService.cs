using Identity.Application.Features.Shared.DTOs;
using Identity.Application.Features.Users.Queries.Login;

namespace Identity.Application.Contracts;

public interface IJwtService
{
    Task<AppUserLoginQueryResponseDto?> GenerateTokenAsync(UserDto result);
}