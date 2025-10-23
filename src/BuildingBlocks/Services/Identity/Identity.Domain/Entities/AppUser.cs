using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    public DateTime Created { get; init; } = DateTime.UtcNow;

    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}