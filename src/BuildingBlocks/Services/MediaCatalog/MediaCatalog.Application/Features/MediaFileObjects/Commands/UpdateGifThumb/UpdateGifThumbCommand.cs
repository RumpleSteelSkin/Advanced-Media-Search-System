using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.UpdateGifThumb;

public record UpdateGifThumbCommand(string ObjectName, string Url) : IRequest<bool>;