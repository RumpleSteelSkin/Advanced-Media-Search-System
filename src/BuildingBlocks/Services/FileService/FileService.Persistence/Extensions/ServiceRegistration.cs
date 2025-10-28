using FileService.Domain.Interfaces;
using FileService.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileService.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService, MinioStorageService>();
    }
}