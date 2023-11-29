using Application.CommentManagement.Dto;
using Application.PostManagement.Dto;
using Application.Shared.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Extensions;

namespace Application.PostManagement.Queries.GetPosts;

public class GetPostsHandler : IRequestHandler<GetPostsRequest, GetPostsResponse>
{
    private readonly HttpClient _httpClient;
    private readonly TypicodeConfig _typicodeConfig;

    public GetPostsHandler(HttpClient httpClient, IOptions<TypicodeConfig> typicodeConfig)
    {
        _httpClient = httpClient;
        _typicodeConfig = typicodeConfig.Value;
    }

    public async Task<GetPostsResponse> Handle(GetPostsRequest request, CancellationToken cancellationToken)
    {
        var posts = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<PostDtoModel>>(_typicodeConfig.PostsApi);

        if (posts == null || !posts.Any())
        {
            throw new KeyNotFoundException("Record not found!");
        }

        if (request.UserId != default)
        {
            posts = posts.FilterByPredicate(x => x.UserId == request.UserId).ToList();
        }

        foreach (var post in posts)
        {
            var postComments = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<CommentDtoModel>>(_typicodeConfig.CommentsApi + $"?postId={post.Id}");

            if (postComments != null && postComments.Any())
            {
                post.Comments = postComments.ToList();
            }
        }

        var pagedPosts = posts.Pagination(request).ToList();

        var response = new GetPostsResponse
        {
            Posts = pagedPosts,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = posts.Count(),
        };

        return response;
    }
}