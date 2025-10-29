using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace FileService.Application.Features.FileRecord.Commands.DeleteFile;

public record DeleteFileCommand(string ObjectName) : IRequest<string>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}