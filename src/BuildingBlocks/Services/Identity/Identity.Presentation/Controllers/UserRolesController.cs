using Core.Application.Base.BaseResult;
using Identity.Application.Features.UserRoles.Commands.Create;
using Identity.Application.Features.UserRoles.Commands.Remove;
using Identity.Application.Features.UserRoles.Commands.Update;
using Identity.Application.Features.UserRoles.Queries.GetAll;
using Identity.Application.Features.UserRoles.Queries.GetByRoleId;
using Identity.Application.Features.UserRoles.Queries.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserRoleController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Create([FromBody] UserRoleCreateCommand command)
    {
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpGet("User/{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<UserRoleGetByUserIdQueryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<UserRoleGetByUserIdQueryResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<UserRoleGetByUserIdQueryResponseDto>>> GetByUserId(Guid id)
    {
        var response = await mediator.Send(new UserRoleGetByUserIdQuery(id));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpGet("Role/{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<UserRoleGetByRoleIdQueryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<UserRoleGetByRoleIdQueryResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<UserRoleGetByRoleIdQueryResponseDto>>> GetById(Guid id)
    {
        var response = await mediator.Send(new UserRoleGetByRoleIdQuery(id));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>),
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<IEnumerable<UserRoleGetAllQueryResponseDto>>>> GetAll()
    {
        var response = await mediator.Send(new UserRoleGetAllQuery());
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }


    [HttpDelete("{userId:guid}/{roleId:guid}")]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResult<object>>> DeleteById(Guid userId, Guid roleId)
    {
        var response = await mediator.Send(new UserRoleRemoveCommand(userId, roleId));
        return response.IsSuccess ? Ok(response) : NotFound(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResult<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<object>>> Update(Guid id, [FromBody] UserRoleUpdateCommand command)
    {
        if (id != command.UserId) return BadRequest(BaseResult<object>.Fail("Id is not equal to Body Id"));
        var response = await mediator.Send(command);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}