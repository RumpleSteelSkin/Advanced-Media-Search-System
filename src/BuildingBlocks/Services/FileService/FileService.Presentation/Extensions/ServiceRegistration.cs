using FileService.Persistence.Settings;

namespace FileService.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddControllers();
        services.Configure<MinioSettings>(configuration.GetSection(nameof(MinioSettings)));
    }
}