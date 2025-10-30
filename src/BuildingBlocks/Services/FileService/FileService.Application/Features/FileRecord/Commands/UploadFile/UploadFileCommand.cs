using Core.Application.Constant;
using Core.Application.Pipelines.Authorization;
using FileService.Application.Features.Shared.DTOs.Files;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.FileRecord.Commands.UploadFile;

public record UploadFileCommand(IFormFile File, CreateFileDto CreateFileDto) : IRequest<string>, IRoleExists
{
    public string[] Roles { get; } = [GeneralRoles.Admin];
}