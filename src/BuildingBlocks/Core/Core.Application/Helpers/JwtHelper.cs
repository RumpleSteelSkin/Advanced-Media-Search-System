using System.Security.Claims;

namespace Core.Application.Helpers;

public static class JwtHelper
{
    public static string? GetUserIdFromToken(ClaimsPrincipal? user)
    {
        return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? user?.FindFirst("sub")?.Value;
    }

    public static string? GetUserEmailFromToken(ClaimsPrincipal? user)
    {
        return user?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static List<string>? GetUserRolesFromToken(ClaimsPrincipal? user)
    {
        var roleClaims = user?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
        if (roleClaims != null && roleClaims.Count != 0) return roleClaims;
        var rolesClaim = user?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        if (rolesClaim == null || string.IsNullOrEmpty(rolesClaim.Value)) return roleClaims;
        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<string>>(rolesClaim.Value);
        }
        catch
        {
            return [rolesClaim.Value];
        }
    }

    public static bool IsTokenExpired(ClaimsPrincipal user)
    {
        var expClaim = user?.FindFirst("exp")?.Value;
        if (!long.TryParse(expClaim, out var expTimestamp)) return true;
        var expiration = DateTimeOffset.FromUnixTimeSeconds(expTimestamp);
        return expiration < DateTimeOffset.UtcNow;
    }
}