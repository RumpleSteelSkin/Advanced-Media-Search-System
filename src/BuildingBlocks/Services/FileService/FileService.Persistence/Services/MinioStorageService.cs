using System.Text;
using Core.Configuration.Settings;
using FileService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using MinioClientFactory = Core.Configuration.Factories.MinioClientFactory;

namespace FileService.Persistence.Services;

public class MinioStorageService(
    MinioClientFactory factory,
    IOptions<MinioSettings> options,
    ILogger<MinioStorageService> logger)
    : IFileStorageService
{
    private readonly IMinioClient _client = factory.CreateClient();
    private readonly MinioSettings _settings = options.Value;

    public async Task<string> UploadAsync(IFormFile file, string? bucketNameExtend = null,
        CancellationToken cancellationToken = default)
    {
        var customBucketName = _settings.BucketName;
        if (!string.IsNullOrEmpty(bucketNameExtend)) customBucketName += bucketNameExtend;
        await EnsureBucketExistsAsync(customBucketName, cancellationToken);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        await using var stream = file.OpenReadStream();

        await _client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(customBucketName)
            .WithObject(fileName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType), cancellationToken);

        logger.LogInformation("File uploaded: {FileName}", fileName);

        return $"{(_settings.WithSsl ? "https" : "http")}://{_settings.Endpoint}/{customBucketName}/{fileName}";
    }

    public async Task DeleteAsync(string fileName, string? bucketNameExtend = null,
        CancellationToken cancellationToken = default)
    {
        var customBucketName = _settings.BucketName;
        if (!string.IsNullOrEmpty(bucketNameExtend)) customBucketName += bucketNameExtend;
        
        await _client.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(customBucketName)
            .WithObject(fileName), cancellationToken);

        logger.LogInformation("File deleted: {FileName}", fileName);
    }

    public async Task<string> GetObjectDetailAsync(string objectName, string? bucketNameExtend = null,
        CancellationToken cancellationToken = default)
    {
        var customBucketName = _settings.BucketName;
        if (!string.IsNullOrEmpty(bucketNameExtend)) customBucketName += bucketNameExtend;
        
        var mediaObject = await _client.StatObjectAsync(
            new StatObjectArgs().WithBucket(customBucketName).WithObject(objectName), cancellationToken);
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("ArchiveStatus: " + mediaObject.ArchiveStatus).AppendLine()
            .Append("ContentType: " + mediaObject.ContentType).AppendLine()
            .Append("Size: " + mediaObject.Size).AppendLine()
            .Append("LastModified: " + mediaObject.LastModified).AppendLine()
            .Append("DeleteMarker: " + mediaObject.DeleteMarker).AppendLine()
            .Append("ETag: " + mediaObject.ETag).AppendLine()
            .Append("Expires: " + mediaObject.Expires).AppendLine()
            .Append("ExtraHeaders: " + string.Join(" - ", mediaObject.ExtraHeaders.Values)).AppendLine()
            .Append("LegalHoldEnabled: " + (mediaObject.LegalHoldEnabled != null && mediaObject.LegalHoldEnabled.Value))
            .AppendLine()
            .Append("MetaData: " + string.Join(" - ", mediaObject.MetaData.Values.ToList())).AppendLine()
            .Append("ObjectLockMode: " + mediaObject.ObjectLockMode).AppendLine()
            .Append("ObjectLockRetainUntilDate: " + mediaObject.ObjectLockRetainUntilDate).AppendLine()
            .Append("ReplicationStatus: " + mediaObject.ReplicationStatus).AppendLine()
            .Append("ObjectName: " + mediaObject.ObjectName).AppendLine()
            .Append("TaggingCount: " + mediaObject.TaggingCount).AppendLine()
            .Append("VersionId: " + mediaObject.VersionId).AppendLine()
            .Append("Object: " + mediaObject).AppendLine();
        return stringBuilder.ToString();
    }

    private async Task EnsureBucketExistsAsync(string bucket, CancellationToken ct)
    {
        var exists = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket), ct);
        if (!exists)
        {
            await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket), ct);
            logger.LogInformation("Bucket created: {Bucket}", bucket);
        }
    }
}