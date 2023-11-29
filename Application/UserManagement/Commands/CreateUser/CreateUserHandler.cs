using Domain.Entities;
using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;

namespace Application.UserManagement.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.Query(x => x.UserName == request.UserName).FirstOrDefaultAsync();

        if (existingUser != null)
        {
            throw new InvalidOperationException("Username is already taken. Please choose a different username.");
        }

        var user = new User
        (
            request.UserName,
            HashHelper.HashPassword(request.Password),
            request.Email,
            new UserProfile(request.FirstName, request.LastName, request.PersonalNumber)
        );

        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}