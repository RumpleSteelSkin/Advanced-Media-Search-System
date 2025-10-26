using Identity.Application.Features.Shared.DTOs;

namespace Identity.Application.Features.UserRoles.Queries.GetByRoleId;

public class UserRoleGetByRoleIdQueryResponseDto
{
    public RoleDto? Role { get; set; }
    public IEnumerable<UserDto>? Users { get; set; }
}