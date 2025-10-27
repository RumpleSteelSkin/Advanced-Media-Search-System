using Core.Application.Base.BaseResult;
using Identity.Application.Features.Users.Commands.RefreshToken;
using Identity.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(typeof(BaseResult<AppUserLoginQueryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AppUserLoginQueryResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<AppUserLoginQueryResponseDto>>> Login([FromBody] AppUserLoginQuery query)
    {
        var response = await mediator.Send(query);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(BaseResult<RefreshTokenCommandResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<RefreshTokenCommandResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
    {
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}