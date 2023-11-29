using Application.Shared;
using MediatR;

namespace Application.PhotoManagement.Queries.GetPhotos;

public class GetPhotosRequest : PaginationRequest, IRequest<GetPhotosResponse>
{
    public int? AlbumId { get; set; }
}