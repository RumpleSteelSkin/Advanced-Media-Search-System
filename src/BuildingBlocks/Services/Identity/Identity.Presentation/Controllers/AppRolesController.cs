using Core.Application.Base.BaseResult;
using Identity.Application.Features.Roles.Commands.Create;
using Identity.Application.Features.Roles.Commands.Remove;
using Identity.Application.Features.Roles.Commands.Update;
using Identity.Application.Features.Roles.Queries.GetAll;
using Identity.Application.Features.Roles.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppRoleController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Create([FromBody] RoleCreateCommand command)
    {
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<RoleGetByIdQueryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<RoleGetByIdQueryResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<RoleGetByIdQueryResponseDto>>> GetById(Guid id)
    {
        var response = await mediator.Send(new RoleGetByIdQuery(id));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<IEnumerable<RoleGetAllQueryResponseDto>>>> GetAll()
    {
        var response = await mediator.Send(new RoleGetAllQuery());
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }


    [HttpDelete(("{id:guid}"))]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<object>>> DeleteById(Guid id)
    {
        var response = await mediator.Send(new RoleRemoveCommand(id));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Update(Guid id, [FromBody] RoleUpdateCommand command)
    {
        if (id != command.Id) return BadRequest(BaseResult<object>.Fail("Id is not equal to Body Id"));
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}