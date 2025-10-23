namespace Core.Application.Pipelines.SoftDelete;

public interface ISoftDelete
{
    Guid Id { get; }
    Type EntityType { get; }
}