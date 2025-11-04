using Core.Infrastructure.Extensions;
using Processing.Application.Consumers;
using Processing.Application.Extensions;

namespace Processing.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddOpenApi();

        services.AddHttpContextAccessor();
        services.AddCoreMassTransitWithConsumers(configuration: configuration,
            typeof(GifCreateEventConsumer));
    }
}