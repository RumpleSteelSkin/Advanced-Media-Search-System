using Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Base.JwtBase;

public abstract class BaseService(IHttpContextAccessor httpContextAccessor)
{
    protected string? CurrentUserId => JwtHelper.GetUserIdFromToken(httpContextAccessor.HttpContext?.User);
    protected string? CurrentUserEmail => JwtHelper.GetUserEmailFromToken(httpContextAccessor.HttpContext?.User);
    protected List<string>? CurrentUserRoles => JwtHelper.GetUserRolesFromToken(httpContextAccessor.HttpContext?.User);

    protected void ValidateUser()
    {
        if (string.IsNullOrEmpty(CurrentUserId))
            throw new UnauthorizedAccessException("User not authenticated");
    }
}