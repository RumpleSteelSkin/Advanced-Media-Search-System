using FileService.Domain.Interfaces;
using MediatR;

namespace FileService.Application.Features.FileRecord.Commands.DeleteFile;

public class DeleteFileCommandHandler(IFileStorageService fileStorageService)
    : IRequestHandler<DeleteFileCommand, string>
{
    public async Task<string> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        await fileStorageService.DeleteAsync(request.ObjectName, cancellationToken);
        return $"{request.ObjectName} was deleted";
    }
}