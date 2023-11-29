#nullable disable

using Application;
using Application.Shared;

namespace Application.PhotoManagement.Dto;

public class PhotoDtoModel : BaseDtoModel<int>
{
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
}
