#nullable disable

using Application.AlbumManagement.Dto;
using Application.Shared;

namespace Application.AlbumManagement.Queries.GetAlbums;

public class GetAlbumsResponse : PaginationResponse
{
    public IEnumerable<AlbumDtoModel> Albums { get; set; }
}