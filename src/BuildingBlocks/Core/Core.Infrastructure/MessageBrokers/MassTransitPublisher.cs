using Core.Application.Interfaces;
using MassTransit;

namespace Core.Infrastructure.MessageBrokers;

public class MassTransitPublisher(IPublishEndpoint publishEndpoint) : IMessagePublisher
{
    public Task PublishAsync<T>(T message) where T : class
    {
        return publishEndpoint.Publish(message);
    }
}