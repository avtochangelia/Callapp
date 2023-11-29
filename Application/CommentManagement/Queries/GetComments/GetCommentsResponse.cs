#nullable disable

using Application.CommentManagement.Dto;
using Application.Shared;

namespace Application.CommentManagement.Queries.GetComments;

public class GetCommentsResponse : PaginationResponse
{
    public IEnumerable<CommentDtoModel> Comments { get; set; }
}