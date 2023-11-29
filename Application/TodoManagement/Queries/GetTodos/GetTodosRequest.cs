using Application.Shared;
using MediatR;

namespace Application.TodoManagement.Queries.GetTodos;

public class GetTodosRequest : PaginationRequest, IRequest<GetTodosResponse>
{
    public int? UserId { get; set; }
}