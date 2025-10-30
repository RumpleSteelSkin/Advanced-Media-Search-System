namespace Core.Domain.DTOs.Events.Files;

public class FileUploadedEvent
{
    public string? ObjectName { get; set; }
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public long Size { get; set; }
    public Guid? UploadedByUserId { get; init; }
}