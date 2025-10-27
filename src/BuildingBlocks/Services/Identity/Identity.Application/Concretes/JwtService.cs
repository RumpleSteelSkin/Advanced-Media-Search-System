using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application.Contracts;
using Identity.Application.Features.Shared.DTOs;
using Identity.Application.Features.Users.Queries.Login;
using Identity.Domain.Entities;
using Identity.Persistence.Context;
using Identity.Persistence.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Concretes;

public class JwtService(
    UserManager<AppUser> userManager,
    IdentityContext context,
    IOptions<JwtTokenOptions> tokenOptions) : IJwtService
{
    public async Task<AppUserLoginQueryResponseDto?> GenerateTokenAsync(UserDto result)
    {
        var user = await userManager.FindByNameAsync(result.UserName ?? string.Empty);
        if (user is null) return null;

        var roles = await userManager.GetRolesAsync(user);
        var dateTimeNow = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Value.Key!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty)
        };
        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Value.Issuer,
            audience: tokenOptions.Value.Audience,
            claims: claims,
            notBefore: dateTimeNow,
            expires: dateTimeNow.AddMinutes(tokenOptions.Value.ExpireInMinute),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        var oldTokens = await context.RefreshTokens
            .Where(r => r.UserId == user.Id && r.Expires < DateTime.UtcNow)
            .ToListAsync();
        if (oldTokens.Count > 0)
        {
            context.RefreshTokens.RemoveRange(oldTokens);
        }

        var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("+", "").Replace("/", "").Replace("=", "");

        var appRefreshToken = new AppRefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            Expires = dateTimeNow.AddDays(7),
            Created = dateTimeNow
        };

        context.RefreshTokens.Add(appRefreshToken);
        await context.SaveChangesAsync();

        return new AppUserLoginQueryResponseDto
        {
            Token = accessToken,
            Expire = jwt.ValidTo,
            RefreshToken = refreshToken,
            RefreshTokenExpire = appRefreshToken.Expires
        };
    }
}