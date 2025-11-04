using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Files;
using FileService.Domain.Interfaces;
using MediatR;

namespace FileService.Application.Features.FileRecord.Commands.UploadGif;

public class UploadGifCommandHandler(IFileStorageService fileStorageService, IMessagePublisher messagePublisher)
    : IRequestHandler<UploadGifCommand, string>
{
    public async Task<string> Handle(UploadGifCommand request, CancellationToken cancellationToken)
    {
        var url = await fileStorageService.UploadAsync(request.File, "thumb", cancellationToken);
        return url;
    }
}