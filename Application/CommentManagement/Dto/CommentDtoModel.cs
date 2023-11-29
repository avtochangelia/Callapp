#nullable disable

using Application.Shared;

namespace Application.CommentManagement.Dto;

public class CommentDtoModel : BaseDtoModel<int>
{
    public int PostId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }
}