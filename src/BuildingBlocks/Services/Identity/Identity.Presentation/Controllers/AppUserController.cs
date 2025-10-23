using Core.Application.Base.BaseResult;
using Identity.Application.Features.Users.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUserController(IMediator mediator) : ControllerBase
{
    [HttpPost("Create")]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Create(AppUserCreateCommand command)
    {
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}