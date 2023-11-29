#nullable disable

using MediatR;

namespace Application.UserManagement.Queries.GetUser;

public class GetUserRequest : IRequest<GetUserResponse>
{
    public int UserId { get; set; }
}