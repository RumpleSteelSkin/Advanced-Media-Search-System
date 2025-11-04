using MediatR;
using Processing.Domain.Interfaces;

namespace Processing.Application.Features.GIF.Commands.Create;

public class CreateGifCommandHandler(IVideoProcessor videoProcessor)
    : IRequestHandler<CreateGifCommand, byte[]?>
{
    public async Task<byte[]?> Handle(CreateGifCommand request, CancellationToken cancellationToken)
    {
        return await videoProcessor.CreateGifAsync(request.VideoUrl);
    }
}