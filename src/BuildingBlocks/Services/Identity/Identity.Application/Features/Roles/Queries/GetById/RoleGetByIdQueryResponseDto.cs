namespace Identity.Application.Features.Roles.Queries.GetById;

public class RoleGetByIdQueryResponseDto
{
    public string? Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}