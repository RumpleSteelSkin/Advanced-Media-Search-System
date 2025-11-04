using Core.Domain.DTOs.Events.Files;
using MassTransit;
using MediaCatalog.Application.Features.MediaFileObjects.Commands.UpdateGifThumb;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediaCatalog.Application.Consumers;

public class GifThumbUploadedEventConsumer(IMediator mediator, ILogger<GifThumbUploadedEventConsumer> logger)
    : IConsumer<GifThumbUploadedEvent>
{
    public async Task Consume(ConsumeContext<GifThumbUploadedEvent> context)
    {
        logger.LogInformation($"{nameof(FileUploadedEventConsumer)} worked for {context.Message.ObjectName}");
        await mediator.Send(new UpdateGifThumbCommand(context.Message.ObjectName, context.Message.Url));
    }
}