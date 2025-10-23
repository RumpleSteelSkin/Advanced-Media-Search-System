using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}