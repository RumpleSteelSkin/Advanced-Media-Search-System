using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Pipelines.SoftDelete;

public class SoftDeletePipeline<TRequest, TResponse>(DbContext context)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ISoftDelete softDeleteCommand)
            return await next(cancellationToken);

        var entityType = softDeleteCommand.EntityType;


        var entity = await context.FindAsync(entityType, [softDeleteCommand.Id], cancellationToken);

        if (entity == null)
            return await next(cancellationToken);

        var isDeletedProp = entityType.GetProperty("IsDeleted");
        var deletedAtProp = entityType.GetProperty("DeletedAt");

        if (isDeletedProp != null)
            isDeletedProp.SetValue(entity, true);
        if (deletedAtProp != null)
            deletedAtProp.SetValue(entity, DateTime.UtcNow);

        await context.SaveChangesAsync(cancellationToken);

        return (TResponse)(object)true;
    }
}