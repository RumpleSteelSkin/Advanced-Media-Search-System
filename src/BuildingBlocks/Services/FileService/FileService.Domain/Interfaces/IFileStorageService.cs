using Microsoft.AspNetCore.Http;

namespace FileService.Domain.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file, string? bucketNameExtend = null,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string fileName, string? bucketNameExtend = null, CancellationToken cancellationToken = default);

    Task<string> GetObjectDetailAsync(string objectName, string? bucketNameExtend = null,
        CancellationToken cancellationToken = default);
}