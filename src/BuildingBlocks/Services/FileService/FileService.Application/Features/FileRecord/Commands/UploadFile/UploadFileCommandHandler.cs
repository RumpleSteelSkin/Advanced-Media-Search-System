using FileService.Domain.Interfaces;
using MediatR;

namespace FileService.Application.Features.FileRecord.Commands.UploadFile;

public class UploadFileCommandHandler(IFileStorageService storageService) : IRequestHandler<UploadFileCommand, string>
{
    public async Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        return await storageService.UploadAsync(request.File, cancellationToken);
    }
}