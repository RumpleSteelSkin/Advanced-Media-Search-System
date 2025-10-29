using Core.Configuration.Settings;
using Core.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Core.Infrastructure.MessageBrokers;

namespace Core.Infrastructure.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddCoreMassTransit(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? configureConsumers = null)
    {
        services.AddMassTransit(opt =>
        {
            configureConsumers?.Invoke(opt);
            opt.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqSettings>>().Value
                                      ?? throw new InvalidOperationException(
                                          "RabbitMqSettings not found in configuration.");
                cfg.Host(
                    host: rabbitMqOptions.HostName,
                    port: rabbitMqOptions.Port,
                    virtualHost: rabbitMqOptions.VirtualHost ?? "/",
                    configure: h =>
                    {
                        h.Username(rabbitMqOptions.Username ?? string.Empty);
                        h.Password(rabbitMqOptions.Password ?? string.Empty);
                    });
                cfg.UseMessageRetry(r => r.Interval(rabbitMqOptions.RetryCount, TimeSpan.FromSeconds(5)));
                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddScoped<IMessagePublisher, MassTransitPublisher>();
        return services;
    }

    public static IServiceCollection AddCoreMassTransitWithConsumers(
        this IServiceCollection services,
        IConfiguration configuration,
        params Type[] consumerTypes)
    {
        return services.AddCoreMassTransit(configuration, opt =>
        {
            foreach (var consumerType in consumerTypes)
            {
                opt.AddConsumer(consumerType);
            }
        });
    }

    public static IServiceCollection AddCoreMassTransitWithConsumers<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : class, IConsumer
    {
        return services.AddCoreMassTransit(configuration, opt => { opt.AddConsumer<T>(); });
    }
}