using Application.Shared;
using MediatR;

namespace Application.AlbumManagement.Queries.GetAlbums;

public class GetAlbumsRequest : PaginationRequest, IRequest<GetAlbumsResponse>
{
    public int? UserId { get; set; }
}