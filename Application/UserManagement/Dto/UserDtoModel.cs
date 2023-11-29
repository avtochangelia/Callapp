#nullable disable

using Application.Shared;

namespace Application.UserManagement.Dto;

public class UserDtoModel : BaseDtoModel<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PersonalNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}