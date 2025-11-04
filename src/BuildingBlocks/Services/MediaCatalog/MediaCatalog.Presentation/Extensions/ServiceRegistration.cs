using Core.Infrastructure.Extensions;
using MediaCatalog.Application.Consumers;
using MediaCatalog.Persistence.Options;

namespace MediaCatalog.Presentation.Extensions;

public static class ServiceRegistration
{
    public static void AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Setting Setter

        services.Configure<MediaCatalogDbSettings>(configuration.GetSection(nameof(MediaCatalogDbSettings)));

        #endregion

        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddOpenApi();
        services.AddAuthorization();
        
        services.AddCoreMassTransitWithConsumers(configuration: configuration, 
            typeof(FileUploadedEventConsumer),
            typeof(GifThumbUploadedEventConsumer));
    }
}