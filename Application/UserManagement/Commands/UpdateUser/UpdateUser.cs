using MediatR;

namespace Application.UserManagement.Commands.UpdateUser;

public record UpdateUser(int Id, string FirstName, string LastName, string PersonalNumber, string UserName, string Password, string Email) : IRequest;