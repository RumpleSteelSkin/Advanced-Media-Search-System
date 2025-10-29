using Core.Infrastructure.Extensions;
using MediaCatalog.Application.Consumers;

namespace MediaCatalog.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddCoreMassTransitWithConsumers<FileUploadedEventConsumer>(configuration);
    }
}