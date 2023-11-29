#nullable disable

using Application.CommentManagement.Dto;
using Application.Shared;

namespace Application.PostManagement.Dto;

public class PostDtoModel : BaseDtoModel<int>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public List<CommentDtoModel> Comments { get; set; }
}