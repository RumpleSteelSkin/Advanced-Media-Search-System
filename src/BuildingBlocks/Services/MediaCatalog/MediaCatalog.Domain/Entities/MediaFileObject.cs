namespace MediaCatalog.Domain.Entities;

public class MediaFileObject
{
    public Guid Id { get; init; }
    public string? Url { get; set; }
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string? ContentType { get; set; }
    public long Size { get; set; }
    public Guid? UploadedByUserId { get; init; }
}