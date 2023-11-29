using MediatR;

namespace Application.UserManagement.Commands.CreateUser;

public record CreateUser(string FirstName, string LastName, string PersonalNumber, string UserName, string Password, string Email) : IRequest;