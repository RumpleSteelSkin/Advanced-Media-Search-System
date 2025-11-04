using System.Reflection;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transactional;
using Core.CrossCuttingConcerns.Loggers.Serilog.Base;
using Core.CrossCuttingConcerns.Loggers.Serilog.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Processing.Domain.Interfaces;
using Processing.Persistence.Concretes;

namespace Processing.Application.Extensions;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVideoProcessor, FfmpegVideoProcessor>();
        
        #region MediatR Services

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(TransactionalPipeline<,>));
            cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));
        });

        #endregion

        #region Serilog Services

        services.AddTransient<LoggerService, MsSqlLogger>();

        #endregion
    }
}