using Application.AlbumManagement.Dto;
using Application.PhotoManagement.Dto;
using Application.Shared.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Shared.Extensions;

namespace Application.AlbumManagement.Queries.GetAlbums;

public class GetAlbumsHandler : IRequestHandler<GetAlbumsRequest, GetAlbumsResponse>
{
    private readonly HttpClient _httpClient;
    private readonly TypicodeConfig _typicodeConfig;

    public GetAlbumsHandler(HttpClient httpClient, IOptions<TypicodeConfig> typicodeConfig)
    {
        _httpClient = httpClient;
        _typicodeConfig = typicodeConfig.Value;
    }

    public async Task<GetAlbumsResponse> Handle(GetAlbumsRequest request, CancellationToken cancellationToken)
    {
        var albums = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<AlbumDtoModel>>(_typicodeConfig.AlbumsApi);

        if (albums == null || !albums.Any())
        {
            throw new KeyNotFoundException("Record not found!");
        }

        if (request.UserId != default)
        {
            albums = albums.FilterByPredicate(x => x.UserId == request.UserId).ToList();
        }

        foreach (var album in albums)
        {
            var albumPhotos = await _httpClient.GetFromJsonWithRetryAsync<IEnumerable<PhotoDtoModel>>(_typicodeConfig.PhotosApi + $"?albumId={album.Id}");

            if (albumPhotos != null && albumPhotos.Any())
            {
                album.Photos = albumPhotos.ToList();
            }
        }

        var pagedAlbums = albums.Pagination(request).ToList();

        var response = new GetAlbumsResponse
        {
            Albums = pagedAlbums,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = albums.Count(),
        };

        return response;
    }
}