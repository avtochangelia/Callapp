using Application.PhotoManagement.Dto;
using Application.Shared.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Extensions;

namespace Application.PhotoManagement.Queries.GetPhotos;

public class GetPhotosHandler : IRequestHandler<GetPhotosRequest, GetPhotosResponse>
{
    private readonly HttpClient _httpClient;
    private readonly TypicodeConfig _typicodeConfig;

    public GetPhotosHandler(HttpClient httpClient, IOptions<TypicodeConfig> typicodeConfig)
    {
        _httpClient = httpClient;
        _typicodeConfig = typicodeConfig.Value;
    }

    public async Task<GetPhotosResponse> Handle(GetPhotosRequest request, CancellationToken cancellationToken)
    {
        var photos = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<PhotoDtoModel>>(_typicodeConfig.PhotosApi);

        if (photos == null || !photos.Any())
        {
            throw new KeyNotFoundException("Record not found!");
        }

        if (request.AlbumId != default)
        {
            photos = photos.FilterByPredicate(x => x.AlbumId == request.AlbumId).ToList();
        }

        var pagedPhotos = photos.Pagination(request).ToList();

        var response = new GetPhotosResponse
        {
            Photos = pagedPhotos,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = photos.Count(),
        };

        return response;
    }
}