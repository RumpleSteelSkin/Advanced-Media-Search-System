using MediaCatalog.Application.Services.Repositories;
using MediaCatalog.Persistence.Context;
using MediaCatalog.Persistence.Options;
using MediaCatalog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MediaCatalog.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        #region Entity Framework Services

        services.AddDbContext<MediaCatalogDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IOptions<MediaCatalogDbSettings>>().Value;
            if (string.IsNullOrWhiteSpace(dbSettings.ConnectionString))
                throw new ArgumentException("You must provide a connection string");
            options.UseNpgsql(dbSettings.ConnectionString);
        });

        #endregion

        #region Repositories

        services.AddScoped<IMediaFileObjectRepository, MediaFileObjectRepository>();

        #endregion
    }
}