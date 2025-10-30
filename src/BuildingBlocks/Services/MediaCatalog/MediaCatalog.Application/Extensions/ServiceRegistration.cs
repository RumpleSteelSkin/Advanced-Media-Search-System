using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transactional;
using Core.CrossCuttingConcerns.Loggers.Serilog.Base;
using Core.CrossCuttingConcerns.Loggers.Serilog.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace MediaCatalog.Application.Extensions;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        #region AutoMapper Services

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        #endregion

        #region MediatR Services

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(TransactionalPipeline<,>));
            cfg.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
            cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));
        });

        #endregion

        #region Serilog Services

        services.AddTransient<LoggerService, MsSqlLogger>();

        #endregion
    }
}