namespace Identity.Application.Features.Users.Queries.GetById;

public class AppUserGetByIdQueryResponseDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? UserName { get; init; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}