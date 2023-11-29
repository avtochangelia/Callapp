#nullable disable

using Application.Shared;
using Application.TodoManagement.Dto;

namespace Application.TodoManagement.Queries.GetTodos;

public class GetTodosResponse : PaginationResponse
{
    public IEnumerable<TodoDtoModel> Todos { get; set; }
}