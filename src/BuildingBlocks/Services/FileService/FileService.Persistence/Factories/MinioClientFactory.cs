using FileService.Persistence.Settings;
using Microsoft.Extensions.Options;
using Minio;

namespace FileService.Persistence.Factories;

public class MinioClientFactory(IOptions<MinioSettings> options)
{
    public IMinioClient CreateClient()
    {
        var builder = new MinioClient().WithEndpoint(options.Value.Endpoint)
            .WithCredentials(options.Value.AccessKey, options.Value.SecretKey);
        if (options.Value.WithSsl)
            builder = builder.WithSSL();
        return builder.Build();
    }
}