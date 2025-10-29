using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.FileRecord.Commands.UploadFile;

public record UploadFileCommand(IFormFile File) : IRequest<string>, IRoleExists
{
    public string? FileName { get; set; }
    public string? Description { get; set; }
    public string[] Roles { get; } = [GeneralRoles.Admin];
}