namespace Core.Persistence.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedTime { get; set; }
    public bool Status { get; set; }
}