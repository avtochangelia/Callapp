#nullable disable

using Application.PostManagement.Dto;
using Application.Shared;

namespace Application.PostManagement.Queries.GetPosts;

public class GetPostsResponse : PaginationResponse
{
    public IEnumerable<PostDtoModel> Posts { get; set; }
}