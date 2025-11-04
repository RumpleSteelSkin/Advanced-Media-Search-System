using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Files;
using Core.Domain.DTOs.Events.Processings;
using FileService.Application.Features.FileRecord.Commands.UploadGif;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FileService.Application.Consumers;

public class GifCreatedEventConsumer(IMediator mediator, IMessagePublisher messagePublisher, ILogger<GifCreatedEventConsumer> logger)
    : IConsumer<GifCreatedEvent>
{
    public async Task Consume(ConsumeContext<GifCreatedEvent> context)
    {
        logger.LogInformation($"{nameof(GifCreatedEventConsumer)} worked for {context.Message.ObjectName}");
        
        using var stream = new MemoryStream(context.Message.GifData);

        IFormFile file = new FormFile(stream, 0, stream.Length, "file", $"{context.Message.ObjectName}.gif")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/gif"
        };

        var response = await mediator.Send(new UploadGifCommand(file));
        await messagePublisher.PublishAsync(new GifThumbUploadedEvent(context.Message.ObjectName, response));
    }
}