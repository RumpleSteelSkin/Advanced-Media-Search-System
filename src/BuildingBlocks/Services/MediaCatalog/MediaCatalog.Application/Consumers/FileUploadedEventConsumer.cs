using Core.Domain.DTOs.Events.Files;
using MassTransit;
using MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;
using MediatR;

namespace MediaCatalog.Application.Consumers;

public class FileUploadedEventConsumer(IMediator mediator) : IConsumer<FileUploadedEvent>
{
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        await mediator.Send(new CreateMediaFileObjectCommand
        {
            Title = context.Message.Title,
            ContentType = context.Message.ContentType,
            FileName = context.Message.FileName,
            UploadedByUserId = context.Message.UploadedByUserId,
            Description = context.Message.Description,
            Size = context.Message.Size,
            Status = true,
            Url = context.Message.Url
        });
    }
}