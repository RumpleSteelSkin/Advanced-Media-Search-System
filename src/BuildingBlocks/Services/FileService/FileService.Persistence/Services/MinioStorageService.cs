using FileService.Domain.Interfaces;
using FileService.Persistence.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using System.Net.Security;

namespace FileService.Persistence.Services;

public class MinioStorageService : IFileStorageService
{
    private readonly MinioSettings _settings;
    private readonly MinioClient? _minio;

    public MinioStorageService(IOptions<MinioSettings> options)
    {
        _settings = options.Value;
        
        var minioBuilder = new MinioClient()
            .WithEndpoint(_settings.Endpoint)
            .WithCredentials(_settings.AccessKey, _settings.SecretKey);
        if (_settings.WithSsl)
        {
            minioBuilder = minioBuilder.WithSSL();
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    {
                        return true; 
                    }
                    return sslPolicyErrors == SslPolicyErrors.None;
                }
            };
            minioBuilder = minioBuilder.WithHttpClient(new HttpClient(httpClientHandler));
        }

        _minio = (MinioClient?)minioBuilder.Build();
    }
    
    public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        if (_minio is null) return "Minio Not Initialized";
        
        var bucketExists = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(_settings.BucketName),
            cancellationToken);
        if (!bucketExists)
            await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(_settings.BucketName), cancellationToken);
        
        await SetPublicReadPolicyAsync(_settings.BucketName, cancellationToken);
        
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        await using var stream = file.OpenReadStream();
        
        await _minio.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(fileName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType), cancellationToken);
        
        var protocol = _settings.WithSsl ? "https" : "http";
        return $"{protocol}://{_settings.Endpoint}/{_settings.BucketName}/{fileName}";
    }

    public async Task<string> SetPublicReadPolicyAsync(string bucketName, CancellationToken cancellationToken = default)
    {
        var policyJson = $$"""

                               {
                                 "Version": "2012-10-17",
                                 "Statement": [
                                   {
                                     "Sid": "PublicReadForGetObjects",
                                     "Effect": "Allow",
                                     "Principal": "*",
                                     "Action": "s3:GetObject",
                                     "Resource": "arn:aws:s3:::{{bucketName}}/*"
                                   }
                                 ]
                               }
                           """;

        try
        {
            if (_minio is null) return "Minio Not Initialized";
            var foundArgs = new BucketExistsArgs().WithBucket(bucketName);
            var found = await _minio.BucketExistsAsync(foundArgs, cancellationToken);
            if (!found)
            {
                var makeArgs = new MakeBucketArgs().WithBucket(bucketName);
                await _minio.MakeBucketAsync(makeArgs, cancellationToken);
                Console.WriteLine($"Bucket '{bucketName}' created.");
            }

            var setPolicyArgs = new SetPolicyArgs().WithBucket(bucketName).WithPolicy(policyJson);
            await _minio.SetPolicyAsync(setPolicyArgs, cancellationToken);
            return $"Bucket '{bucketName}' policy is Public Read-Only.";
        }
        catch (Exception ex)
        {
            return $"An error occured {ex.Message}";
        }
    }
}