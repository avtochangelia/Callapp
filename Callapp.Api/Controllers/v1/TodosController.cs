using Application.TodoManagement.Queries.GetTodos;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Api.Controllers.v1;

[Route("api/v1/todos")]
public class TodosController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetTodosResponse>> GetAllasync([FromQuery] int? userId, int? page, int? pageSize)
    {
        var request = new GetTodosRequest
        {
            UserId = userId,
            Page = page,
            PageSize = pageSize,
        };

        return Ok(await Mediator.Send(request));
    }
}