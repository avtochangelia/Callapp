using MediatR;

namespace Application.UserManagement.Commands.ActivateUser;

public class ActivateUser : IRequest
{
    public int UserId { get; set; }
}