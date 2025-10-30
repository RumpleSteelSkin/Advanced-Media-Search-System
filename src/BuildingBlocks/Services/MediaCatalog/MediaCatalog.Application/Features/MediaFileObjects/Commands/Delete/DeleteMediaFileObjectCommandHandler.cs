using Core.Application.Base.BaseResult;
using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Files;
using MediaCatalog.Application.Services.Repositories;
using MediatR;

namespace MediaCatalog.Application.Features.MediaFileObjects.Commands.Delete;

public class DeleteMediaFileObjectCommandHandler(
    IMediaFileObjectRepository repository,
    IMessagePublisher messagePublisher)
    : IRequestHandler<DeleteMediaFileObjectCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(DeleteMediaFileObjectCommand request,
        CancellationToken cancellationToken)
    {
        var mediaFileObject = await repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        if (mediaFileObject == null) return BaseResult<object>.NotFound("Media file object was not found");
        if (mediaFileObject.ObjectName is null) return BaseResult<object>.Fail("Media file object was not deleted");
        await repository.DeleteAsync(mediaFileObject, cancellationToken);
        var deletedEvent = new FileRemoveEvent(mediaFileObject.ObjectName);
        await messagePublisher.PublishAsync(deletedEvent);
        return BaseResult<object>.Success("Media file objects removed");
    }
}