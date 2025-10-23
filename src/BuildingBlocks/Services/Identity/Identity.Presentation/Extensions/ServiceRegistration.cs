using System.Text.Json.Serialization;
using Identity.Persistence.Options;

namespace Identity.Presentation.Extensions;

public static class ServiceRegistration
{
    private static readonly string[] ConfigureOptions = ["en", "tr"];

    public static IServiceCollection AddPresentationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Setting Setter

        services.Configure<IdentityDbConnection>(configuration.GetSection(nameof(IdentityDbConnection)));

        #endregion

        #region CORS Services

        services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        #endregion
        
        #region JSON Cycle Ignore

        services.ConfigureHttpJsonOptions(config =>
        {
            config.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        #endregion

        #region Other Services

        services.AddOpenApi();
        services.AddAuthorization();
        services.AddControllers();

        #endregion

        return services;
    }
}