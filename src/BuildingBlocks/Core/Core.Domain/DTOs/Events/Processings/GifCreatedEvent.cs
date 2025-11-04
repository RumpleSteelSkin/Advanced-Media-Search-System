namespace Core.Domain.DTOs.Events.Processings;

public class GifCreatedEvent
{
    public string ObjectName { get; set; } = null!;
    public byte[] GifData { get; set; } = null!;
}