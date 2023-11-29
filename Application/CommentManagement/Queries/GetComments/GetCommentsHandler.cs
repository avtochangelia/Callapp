using Application.CommentManagement.Dto;
using Application.Shared.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Extensions;

namespace Application.CommentManagement.Queries.GetComments;

public class GetCommentsHandler : IRequestHandler<GetCommentsRequest, GetCommentsResponse>
{
    private readonly HttpClient _httpClient;
    private readonly TypicodeConfig _typicodeConfig;

    public GetCommentsHandler(HttpClient httpClient, IOptions<TypicodeConfig> typicodeConfig)
    {
        _httpClient = httpClient;
        _typicodeConfig = typicodeConfig.Value;
    }

    public async Task<GetCommentsResponse> Handle(GetCommentsRequest request, CancellationToken cancellationToken)
    {
        var comments = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<CommentDtoModel>>(_typicodeConfig.CommentsApi);

        if (comments == null || !comments.Any())
        {
            throw new KeyNotFoundException("Record not found!");
        }

        if (request.PostId != default)
        {
            comments = comments.FilterByPredicate(x => x.PostId == request.PostId).ToList();
        }

        var pagedComments = comments.Pagination(request).ToList();

        var response = new GetCommentsResponse
        {
            Comments = pagedComments,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = comments.Count(),
        };

        return response;
    }
}