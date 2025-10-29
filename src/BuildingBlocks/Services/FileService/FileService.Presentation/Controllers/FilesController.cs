using FileService.Application.Features.FileRecord.Commands.DeleteFile;
using FileService.Application.Features.FileRecord.Commands.UploadFile;
using FileService.Application.Features.Queries.GetFileObjectDetail;
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

    [HttpDelete("delete/{objectName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(string objectName, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new DeleteFileCommand(objectName), cancellationToken));
    }

    [HttpGet("{objectName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetObjectDetail(string objectName, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetFileObjectDetailByObjectNameQuery(objectName), cancellationToken));
    }
}