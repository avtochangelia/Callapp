using Application.UserManagement.Commands.ActivateUser;
using Application.UserManagement.Commands.CreateUser;
using Application.UserManagement.Commands.DeactivateUser;
using Application.UserManagement.Commands.DeleteUser;
using Application.UserManagement.Commands.UpdateUser;
using Application.UserManagement.Queries.GetUser;
using Application.UserManagement.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Api.Controllers.v1;

[Route("api/v1/users")]
public class UsersController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUsersResponse>> GetAllasync([FromQuery] int? page, int? pageSize)
    {
        var request = new GetUsersRequest
        {
            Page = page,
            PageSize = pageSize,
        };

        return Ok(await Mediator.Send(request));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserResponse>> GetAsync(int id)
    {
        return Ok(await Mediator.Send(new GetUserRequest { UserId = id }));
    }

    [HttpPost]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUser command)
    {
        _ = await Mediator.Send(command);

        return StatusCode(201);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateUser command)
    {
        await Mediator.Send(command);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var command = new DeleteUser
        {
            UserId = id,
        };

        _ = await Mediator.Send(command);

        return Ok();
    }

    [HttpPatch("{id}/activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ActivateAsync(int id)
    {
        _ = await this.Mediator.Send(new ActivateUser { UserId = id });

        return this.Ok();
    }

    [HttpPatch("{id}/Deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeactivateAsync(int id)
    {
        _ = await this.Mediator.Send(new DeactivateUser { UserId = id });

        return this.Ok();
    }
}