using MediatR;

namespace Application.UserManagement.Commands.DeleteUser;

public class DeleteUser : IRequest
{
    public int UserId { get; set; }
}