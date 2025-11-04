using Core.Domain.DTOs.Events.Files;
using FileService.Application.Features.FileRecord.Commands.DeleteFile;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FileService.Application.Consumers;

public class FileDeletedEventConsumer(IMediator mediator, ILogger<FileDeletedEventConsumer> logger)
    : IConsumer<FileRemoveEvent>
{
    public async Task Consume(ConsumeContext<FileRemoveEvent> context)
    {
        logger.LogInformation($"{nameof(FileDeletedEventConsumer)} worked for {context.Message.ObjectName}");
        await mediator.Send(new DeleteFileCommand(context.Message.ObjectName));
    }
}