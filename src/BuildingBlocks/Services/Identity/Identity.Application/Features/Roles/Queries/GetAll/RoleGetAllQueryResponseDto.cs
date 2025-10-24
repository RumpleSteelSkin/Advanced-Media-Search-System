namespace Identity.Application.Features.Roles.Queries.GetAll;

public class RoleGetAllQueryResponseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}