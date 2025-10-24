using Core.Application.Base.BaseResult;
using Identity.Application.Features.Users.Commands.Create;
using Identity.Application.Features.Users.Commands.Remove;
using Identity.Application.Features.Users.Commands.Update;
using Identity.Application.Features.Users.Queries.GetAll;
using Identity.Application.Features.Users.Queries.GetById;
using Identity.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppUserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Create([FromBody] AppUserCreateCommand command)
    {
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<AppUserGetByIdQueryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AppUserGetByIdQueryResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<AppUserGetByIdQueryResponseDto>>> GetById(Guid id)
    {
        var response = await mediator.Send(new AppUserGetByIdQuery(id));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<IEnumerable<AppUserGetAllQueryResponseDto>>>> GetAll()
    {
        var response = await mediator.Send(new AppUserGetAllQuery());
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(BaseResult<AppUserLoginQueryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<AppUserLoginQueryResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<AppUserLoginQueryResponseDto>>> Login([FromBody] AppUserLoginQuery query)
    {
        var response = await mediator.Send(query);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpDelete(("{id:guid}"))]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<object>>> DeleteById(Guid id)
    {
        var response = await mediator.Send(new AppUserRemoveCommand(id));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Update(Guid id, [FromBody] AppUserUpdateCommand command)
    {
        if (id != command.Id) return BadRequest(BaseResult<object>.Fail("Id is not equal to Body Id"));
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}