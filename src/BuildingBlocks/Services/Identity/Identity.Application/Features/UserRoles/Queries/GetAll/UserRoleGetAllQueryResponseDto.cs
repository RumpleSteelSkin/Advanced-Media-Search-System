using Identity.Application.Features.Shared.DTOs;

namespace Identity.Application.Features.UserRoles.Queries.GetAll;

public class UserRoleGetAllQueryResponseDto
{
    public UserDto? User { get; set; }
    public List<RoleDto>? Roles { get; set; }
}