using FileService.Persistence.Settings;

namespace FileService.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddOpenApi();
        services.AddControllers();
        services.AddAuthorization();
        services.Configure<MinioSettings>(configuration.GetSection(nameof(MinioSettings)));
    }
}