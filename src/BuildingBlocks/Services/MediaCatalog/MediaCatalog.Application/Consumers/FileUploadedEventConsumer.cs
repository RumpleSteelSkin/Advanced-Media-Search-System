using Core.Domain.DTOs.Events.Files;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MediaCatalog.Application.Consumers;

public class FileUploadedEventConsumer(ILogger<FileUploadedEventConsumer> logger):IConsumer<FileUploadedEvent>
{
    public Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        logger.LogInformation("FileUploadedEventConsumer Consumed");
        logger.LogInformation(context.Message.FileName);
        return Task.CompletedTask;
    }
}