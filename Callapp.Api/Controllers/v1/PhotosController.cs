using Application.PhotoManagement.Queries.GetPhotos;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Api.Controllers.v1;

[Route("api/v1/photos")]
public class PhotosController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPhotosResponse>> GetAllasync([FromQuery] int? albumId, int? page, int? pageSize)
    {
        var request = new GetPhotosRequest
        {
            AlbumId = albumId,
            Page = page,
            PageSize = pageSize,
        };

        return Ok(await Mediator.Send(request));
    }
}