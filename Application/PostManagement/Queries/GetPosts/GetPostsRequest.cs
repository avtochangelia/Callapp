using Application.Shared;
using MediatR;

namespace Application.PostManagement.Queries.GetPosts;

public class GetPostsRequest : PaginationRequest, IRequest<GetPostsResponse>
{
    public int? UserId { get; set; }
}