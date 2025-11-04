using Core.Infrastructure.Extensions;
using FileService.Application.Consumers;

namespace FileService.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddOpenApi();
        services.AddControllers();
        services.AddAuthorization();
        services.AddCoreMassTransitWithConsumers(configuration: configuration, typeof(FileDeletedEventConsumer),
            typeof(GifCreatedEventConsumer));
    }
}