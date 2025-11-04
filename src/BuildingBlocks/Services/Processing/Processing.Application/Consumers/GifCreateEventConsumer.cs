using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Processings;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Processing.Application.Features.GIF.Commands.Create;

namespace Processing.Application.Consumers;

public class GifCreateEventConsumer(
    IMediator mediator,
    IMessagePublisher massTransitPublisher,
    ILogger<GifCreateEventConsumer> logger)
    : IConsumer<GifCreateEvent>
{
    public async Task Consume(ConsumeContext<GifCreateEvent> context)
    {
        logger.LogInformation($"{nameof(GifCreateEventConsumer)} worked for {context.Message.ObjectName}");
        var gifBytes = await mediator.Send(new CreateGifCommand(context.Message.VideoUrl));
        if (gifBytes != null && context.Message.ObjectName != null)
        {
            await massTransitPublisher.PublishAsync(new GifCreatedEvent
            {
                ObjectName = context.Message.ObjectName,
                GifData = gifBytes,
            });
        }
    }
}