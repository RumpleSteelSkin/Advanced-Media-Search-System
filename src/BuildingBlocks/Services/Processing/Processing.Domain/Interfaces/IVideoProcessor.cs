namespace Processing.Domain.Interfaces;

public interface IVideoProcessor
{
    Task<byte[]> CreateGifAsync(string videoUrl);
}