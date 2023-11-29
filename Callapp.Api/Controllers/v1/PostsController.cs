using Application.PostManagement.Queries.GetPosts;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Api.Controllers.v1
{
    [Route("api/v1/posts")]
    public class PostsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPostsResponse>> GetAllasync([FromQuery] int? userId, int? page, int? pageSize)
        {
            var request = new GetPostsRequest
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
            };

            return Ok(await Mediator.Send(request));
        }
    }
}