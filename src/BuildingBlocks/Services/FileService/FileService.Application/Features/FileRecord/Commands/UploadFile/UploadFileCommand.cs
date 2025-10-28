using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.FileRecord.Commands.UploadFile;

public record UploadFileCommand(IFormFile File) : IRequest<string>;