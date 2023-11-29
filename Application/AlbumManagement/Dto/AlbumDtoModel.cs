#nullable disable

using Application.PhotoManagement.Dto;

namespace Application.AlbumManagement.Dto;

public class AlbumDtoModel
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public List<PhotoDtoModel> Photos { get; set; }
}