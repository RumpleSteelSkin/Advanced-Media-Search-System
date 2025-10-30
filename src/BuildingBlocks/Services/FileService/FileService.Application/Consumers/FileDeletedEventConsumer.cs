using Core.Domain.DTOs.Events.Files;
using FileService.Application.Features.FileRecord.Commands.DeleteFile;
using MassTransit;
using MediatR;

namespace FileService.Application.Consumers;

public class FileDeletedEventConsumer(IMediator mediator) : IConsumer<FileRemoveEvent>
{
    public async Task Consume(ConsumeContext<FileRemoveEvent> context)
    {
        await mediator.Send(new DeleteFileCommand(context.Message.ObjectName));
    }
}