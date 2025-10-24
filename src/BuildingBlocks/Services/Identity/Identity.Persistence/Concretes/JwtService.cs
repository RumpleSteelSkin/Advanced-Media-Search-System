using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application.Contracts.Persistence;
using Identity.Application.Features.Shared.DTOs;
using Identity.Application.Features.Users.Queries.Login;
using Identity.Domain.Entities;
using Identity.Persistence.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Persistence.Concretes;

public class JwtService(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IOptions<JwtTokenOptions> tokenOptions) : IJwtService
{
    public async Task<AppUserLoginQueryResponseDto?> GenerateTokenAsync(UserDto result)
    {
        var user = await userManager.FindByNameAsync(result.UserName ?? string.Empty);
        if (user is null) return null;

        var roles = await userManager.GetRolesAsync(user);

        var hasDeletedRole = await roleManager.Roles
            .Where(r => r.IsDeleted && r.Name != null && roles.Contains(r.Name))
            .AnyAsync();

        if (hasDeletedRole)
            return new AppUserLoginQueryResponseDto
                { Token = "You do not have permission, a role has been deleted, contact the administrator." };

        var dateTimeNow = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.Key!));

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new("fullName", string.Join(" ", user.FirstName, user.LastName))
        ];

        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Value.Issuer,
            audience: tokenOptions.Value.Audience,
            claims: claims,
            notBefore: dateTimeNow.AddSeconds(-5),
            expires: dateTimeNow.AddMinutes(Math.Max(1, tokenOptions.Value.ExpireInMinute)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new AppUserLoginQueryResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            Expire = dateTimeNow.AddMinutes(tokenOptions.Value.ExpireInMinute)
        };
    }
}