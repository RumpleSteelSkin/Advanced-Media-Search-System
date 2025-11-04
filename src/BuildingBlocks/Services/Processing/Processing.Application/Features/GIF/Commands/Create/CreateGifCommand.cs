using MediatR;

namespace Processing.Application.Features.GIF.Commands.Create;

public record CreateGifCommand(string? VideoUrl) : IRequest<byte[]?>;