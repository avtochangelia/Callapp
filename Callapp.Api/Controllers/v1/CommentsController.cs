using Application.CommentManagement.Queries.GetComments;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Api.Controllers.v1
{
    [Route("api/v1/comments")]
    public class CommentsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCommentsResponse>> GetAllasync([FromQuery] int? postId, int? page, int? pageSize)
        {
            var request = new GetCommentsRequest
            {
                PostId = postId,
                Page = page,
                PageSize = pageSize,
            };

            return Ok(await Mediator.Send(request));
        }
    }
}