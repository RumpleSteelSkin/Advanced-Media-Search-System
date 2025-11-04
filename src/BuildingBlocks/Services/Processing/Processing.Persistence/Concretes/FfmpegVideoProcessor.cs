using FFMpegCore;
using Processing.Domain.Interfaces;

namespace Processing.Persistence.Concretes;

public class FfmpegVideoProcessor : IVideoProcessor
{
    public FfmpegVideoProcessor()
    {
        var ffmpegPath = Path.Combine(AppContext.BaseDirectory, "ffmpeg");
        GlobalFFOptions.Configure(new FFOptions
        {
            BinaryFolder = ffmpegPath,
            TemporaryFilesFolder = Path.GetTempPath()
        });
    }

    public async Task<byte[]> CreateGifAsync(string videoUrl)
    {
        var tempDir = Path.GetTempPath();
        var tempVideoPath = Path.Combine(tempDir, $"{Guid.NewGuid()}.mp4");
        var outputGifPath = Path.Combine(tempDir, $"{Guid.NewGuid()}.gif");

        using var httpClient = new HttpClient();
        var videoBytes = await httpClient.GetByteArrayAsync(videoUrl);
        await File.WriteAllBytesAsync(tempVideoPath, videoBytes);

        var mediaInfo = await FFProbe.AnalyseAsync(tempVideoPath);
        var totalDuration = mediaInfo.Duration.TotalSeconds;

        var durationSeconds = Math.Min(totalDuration, 10);

        await FFMpegArguments
            .FromFileInput(tempVideoPath)
            .OutputToFile(outputGifPath, overwrite: true, options => options
                .WithVideoCodec("gif")
                .WithCustomArgument($"-t {durationSeconds}")
                .WithCustomArgument("-vf fps=10,scale=320:-1:flags=lanczos")
            )
            .ProcessAsynchronously();

        var gifBytes = await File.ReadAllBytesAsync(outputGifPath);

        File.Delete(tempVideoPath);
        File.Delete(outputGifPath);

        return gifBytes;
    }
}