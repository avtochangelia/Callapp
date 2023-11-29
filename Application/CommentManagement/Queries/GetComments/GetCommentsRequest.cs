using Application.Shared;
using MediatR;

namespace Application.CommentManagement.Queries.GetComments;

public class GetCommentsRequest : PaginationRequest, IRequest<GetCommentsResponse>
{
    public int? PostId { get; set; }
}