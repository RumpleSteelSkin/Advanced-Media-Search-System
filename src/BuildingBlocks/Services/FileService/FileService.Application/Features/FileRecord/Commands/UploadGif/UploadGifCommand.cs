using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileService.Application.Features.FileRecord.Commands.UploadGif;

public record UploadGifCommand(IFormFile File) : IRequest<string>;