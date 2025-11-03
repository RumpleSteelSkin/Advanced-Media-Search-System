using FileService.Application.Features.FileRecord.Commands.UploadFile;
using FileService.Application.Features.FileRecord.Queries.GetFileObjectDetail;
using FileService.Application.Features.Shared.DTOs.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController(IMediator mediator) : ControllerBase
{
    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload(IFormFile? file, [FromForm] string? title, [FromForm] string? description,
        CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0) return BadRequest("File not uploaded");
        CreateFileDto createFileDto = new(title, description);
        var url = await mediator.Send(new UploadFileCommand(file, createFileDto), cancellationToken);
        return Ok(new { Url = url });
    }

    [HttpGet("{objectName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetObjectDetail(string objectName, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetFileObjectDetailByObjectNameQuery(objectName), cancellationToken));
    }
}