using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transactional;
using Microsoft.Extensions.DependencyInjection;

namespace FileService.Application.Extensions;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // cfg.AddOpenBehavior(typeof(TransactionalPipeline<,>));
            // cfg.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
            // cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));
        });
    }
}