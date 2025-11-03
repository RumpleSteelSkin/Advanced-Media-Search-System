using MediatR;

namespace FileService.Application.Features.FileRecord.Commands.DeleteFile;

public record DeleteFileCommand(string? ObjectName) : IRequest<string>;