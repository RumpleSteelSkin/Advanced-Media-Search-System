using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transactional;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Loggers.Serilog.Base;
using Core.CrossCuttingConcerns.Loggers.Serilog.Loggers;
using FluentValidation;
using Identity.Application.Concretes;
using Identity.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(TransactionalPipeline<,>));
            cfg.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
            cfg.AddOpenBehavior(typeof(ValidationPipeline<,>));
            cfg.AddOpenBehavior(typeof(LoggingPipeline<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IJwtService, JwtService>();
        #region Serilog Services

        services.AddTransient<LoggerService, MsSqlLogger>();

        #endregion
    }
}