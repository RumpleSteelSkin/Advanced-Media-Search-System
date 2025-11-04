using MediaCatalog.Application.Services.Repositories;
using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.UpdateGifThumb;

public class UpdateGifThumbCommandHandler(IMediaFileObjectRepository repository)
    : IRequestHandler<UpdateGifThumbCommand, bool>
{
    public async Task<bool> Handle(UpdateGifThumbCommand request, CancellationToken cancellationToken)
    {
        var item = await repository.GetAsync(x => x.ObjectName == request.ObjectName,
            cancellationToken: cancellationToken);
        if (item is null) return false;
        item.ThumbUrl = request.Url;
        await repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}