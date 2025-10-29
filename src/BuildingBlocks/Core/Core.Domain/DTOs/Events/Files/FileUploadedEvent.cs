namespace Core.Domain.DTOs.Events.Files;

public class FileUploadedEvent
{
    public Guid FileId { get; init; } = Guid.NewGuid();
    public string? Url { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public long Size { get; set; }
    public Guid? UploadedByUserId { get; init; }
}