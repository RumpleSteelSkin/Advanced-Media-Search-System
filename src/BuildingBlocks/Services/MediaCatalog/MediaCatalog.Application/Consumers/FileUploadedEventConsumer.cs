using Core.Application.Base.BaseResult;
using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Files;
using Core.Domain.DTOs.Events.Processings;
using MassTransit;
using MediaCatalog.Application.Features.MediaFileObjects.Commands.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediaCatalog.Application.Consumers;

public class FileUploadedEventConsumer(
    IMediator mediator,
    IMessagePublisher messagePublisher,
    ILogger<FileUploadedEventConsumer> logger)
    : IConsumer<FileUploadedEvent>
{
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        logger.LogInformation($"{nameof(FileUploadedEventConsumer)} worked for {context.Message.ObjectName}");
        var baseResult = (await mediator.Send(new CreateMediaFileObjectCommand
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
        }));
        
        if (baseResult.IsSuccess)
        {
            await messagePublisher.PublishAsync(new GifCreateEvent(context.Message.ObjectName, context.Message.Url));
        }
        else
        {
            await messagePublisher.PublishAsync(new FileRemoveEvent(context.Message.ObjectName));
        }
    }
}