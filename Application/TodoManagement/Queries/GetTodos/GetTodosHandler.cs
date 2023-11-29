using Application.Shared.Configurations;
using Application.TodoManagement.Dto;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Extensions;

namespace Application.TodoManagement.Queries.GetTodos;

public class GetTodosHandler : IRequestHandler<GetTodosRequest, GetTodosResponse>
{
    private readonly HttpClient _httpClient;
    private readonly TypicodeConfig _typicodeConfig;

    public GetTodosHandler(HttpClient httpClient, IOptions<TypicodeConfig> typicodeConfig)
    {
        _httpClient = httpClient;
        _typicodeConfig = typicodeConfig.Value;
    }

    public async Task<GetTodosResponse> Handle(GetTodosRequest request, CancellationToken cancellationToken)
    {
        var todos = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<TodoDtoModel>>(_typicodeConfig.TodosApi);

        if (todos == null || !todos.Any())
        {
            throw new KeyNotFoundException("Record not found!");
        }

        if (request.UserId != default)
        {
            todos = todos.FilterByPredicate(x => x.UserId == request.UserId).ToList();
        }
        
        var pagedTodos = todos.Pagination(request).ToList();

        var response = new GetTodosResponse
        {
            Todos = pagedTodos,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = todos.Count(),
        };

        return response;
    }
}