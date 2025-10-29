using Core.Application.Helpers;
using Core.Application.Interfaces;
using Core.Domain.DTOs.Events.Files;
using FileService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.FileRecord.Commands.UploadFile;

public class UploadFileCommandHandler(
    IFileStorageService storageService,
    IMessagePublisher messagePublisher,
    IHttpContextAccessor accessor)
    : IRequestHandler<UploadFileCommand, string>
{
    public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var url = await storageService.UploadAsync(request.File, cancellationToken);

        var uploadedEvent = new FileUploadedEvent
        {
            FileName = request.FileName ?? request.File.FileName,
            ContentType = request.File.ContentType,
            Url = url,
            Description = request.Description ?? string.Empty,
            Size = request.File.Length,
            UploadedByUserId = Guid.Parse(JwtHelper.GetUserIdFromToken(accessor.HttpContext?.User) ?? string.Empty)
        };

        await messagePublisher.PublishAsync(uploadedEvent);

        return url;
    }
}