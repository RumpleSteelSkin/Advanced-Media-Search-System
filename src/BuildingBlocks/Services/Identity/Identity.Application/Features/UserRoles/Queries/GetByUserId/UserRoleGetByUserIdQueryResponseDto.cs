using Identity.Application.Features.Shared.DTOs;

namespace Identity.Application.Features.UserRoles.Queries.GetByUserId;

public class UserRoleGetByUserIdQueryResponseDto
{
    public UserDto? User { get; set; }
    public List<RoleDto>? Roles { get; set; }
}