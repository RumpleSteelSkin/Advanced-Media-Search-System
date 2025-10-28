using FileService.Application.Features.FileRecord.Commands.UploadFile;
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
    public async Task<IActionResult> Upload(IFormFile? file, CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0) return BadRequest("File not uploaded");
        var url = await mediator.Send(new UploadFileCommand(file), cancellationToken);
        return Ok(new { Url = url });
    }
}