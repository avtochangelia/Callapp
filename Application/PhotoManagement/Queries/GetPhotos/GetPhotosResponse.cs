#nullable disable

using Application.PhotoManagement.Dto;
using Application.Shared;

namespace Application.PhotoManagement.Queries.GetPhotos;

public class GetPhotosResponse : PaginationResponse
{
    public IEnumerable<PhotoDtoModel> Photos { get; set; }
}