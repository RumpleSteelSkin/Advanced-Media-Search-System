using System.Reflection;
using Core.Configuration.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddCoreSharedConfiguration(this IConfigurationBuilder builder)
    {
        var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var sharedFilePath = Path.Combine(assemblyLocation, "appsettings.shared.json");

        if (!File.Exists(sharedFilePath))
            throw new FileNotFoundException($"Shared configuration file not found: {sharedFilePath}");

        builder.AddJsonFile(sharedFilePath, false, true);
        builder.AddEnvironmentVariables();
        return builder;
    }


    public static IServiceCollection AddCoreConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtTokenOptions>(configuration.GetSection(nameof(JwtTokenOptions)));
        services.Configure<SerilogLogConfigurations>(configuration.GetSection(nameof(SerilogLogConfigurations)));
        services.Configure<MinioSettings>(configuration.GetSection(nameof(MinioSettings)));
        services.Configure<RabbitMqSettings>(configuration.GetSection(nameof(RabbitMqSettings)));
        
        return services;
    }
}