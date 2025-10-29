namespace FileService.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddOpenApi();
        services.AddControllers();
        services.AddAuthorization();
    }
}