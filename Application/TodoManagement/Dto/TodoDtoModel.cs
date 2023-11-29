#nullable disable

using Application.Shared;

namespace Application.TodoManagement.Dto;

public class TodoDtoModel : BaseDtoModel<int>
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public bool Completed { get; set; }
}