using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Files;
using MassTransit;
using MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;
using MediatR;

namespace MediaCatalog.Application.Consumers;

public class FileUploadedEventConsumer(IMediator mediator, IMessagePublisher messagePublisher)
    : IConsumer<FileUploadedEvent>
{
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        if (!(await mediator.Send(new CreateMediaFileObjectCommand
            {
                ObjectName = context.Message.ObjectName,
                Title = context.Message.Title,
                ContentType = context.Message.ContentType,
                FileName = context.Message.FileName,
                UploadedByUserId = context.Message.UploadedByUserId,
                Description = context.Message.Description,
                Size = context.Message.Size,
                Status = true,
                Url = context.Message.Url
            })).IsSuccess)
        {
            await messagePublisher.PublishAsync(new FileRemoveEvent(context.Message.ObjectName));
        }
    }
}