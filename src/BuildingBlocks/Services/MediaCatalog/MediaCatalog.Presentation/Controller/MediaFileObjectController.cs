using MediaCatalog.Application.Features.MediaFileObjects.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaCatalog.Presentation.Controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MediaFileObjectController(IMediator mediator) : ControllerBase
{
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFile(Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        var response = await mediator.Send(new DeleteMediaFileObjectCommand(id));
        return response.IsSuccess ? NoContent() : NotFound(response);
    }
}