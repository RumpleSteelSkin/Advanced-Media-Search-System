using System.Security.Claims;
using System.Text.Json;
using Core.CrossCuttingConcerns.Loggers.Serilog.Base;
using Core.CrossCuttingConcerns.Loggers.Serilog.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Logging;

public class LoggingPipeline<TRequest, TResponse>(LoggerService logger, IHttpContextAccessor contextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var log = new LogDetail
        {
            Project = typeof(TRequest).AssemblyQualifiedName?.Split(',').FirstOrDefault(),
            MethodName = typeof(TRequest).Name,
            Parameters = [new LogParameter { Type = request.GetType().Name, Value = request.ToString() }],
            User = contextAccessor.HttpContext?.User?.Claims
                       .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value
                   ?? "Anonymous"
        };

        logger.Info(JsonSerializer.Serialize(log, JsonOptionsCache.Default));
        return await next(cancellationToken);
    }
}