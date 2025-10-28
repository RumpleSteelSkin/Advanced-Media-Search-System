namespace FileService.Domain.Entities;

public class FileRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}