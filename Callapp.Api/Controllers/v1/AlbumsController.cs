using Application.AlbumManagement.Queries.GetAlbums;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Api.Controllers.v1;

[Route("api/v1/albums")]
public class AlbumsController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAlbumsResponse>> GetAllasync([FromQuery] int? userId, int? page, int? pageSize)
    {
        var request = new GetAlbumsRequest
        {
            UserId = userId,
            Page = page,
            PageSize = pageSize,
        };

        return Ok(await Mediator.Send(request));
    }
}