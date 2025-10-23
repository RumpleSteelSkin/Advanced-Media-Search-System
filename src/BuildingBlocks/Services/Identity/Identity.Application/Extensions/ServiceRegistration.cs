using System.Reflection;
using Core.Application.Pipelines.SoftDelete;
using Core.Application.Pipelines.Transactional;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(TransactionalPipeline<,>));
            cfg.AddOpenBehavior(typeof(ValidationPipeline<,>));
            cfg.AddOpenBehavior(typeof(SoftDeletePipeline<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        return services;
    }
}