using MediatR;

namespace Application.UserManagement.Commands.DeactivateUser;

public class DeactivateUser : IRequest
{
    public int UserId { get; set; }
}