using Microsoft.AspNetCore.Http;

namespace FileService.Domain.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);

    Task<string> SetPublicReadPolicyAsync(string bucketName, CancellationToken cancellationToken = default);
}