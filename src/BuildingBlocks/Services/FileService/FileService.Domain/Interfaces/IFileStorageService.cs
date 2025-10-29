using Microsoft.AspNetCore.Http;

namespace FileService.Domain.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default);
    Task DeleteAsync(string fileName, CancellationToken cancellationToken = default);
    Task<string> GetObjectDetailAsync(string objectName, CancellationToken cancellationToken = default);
}