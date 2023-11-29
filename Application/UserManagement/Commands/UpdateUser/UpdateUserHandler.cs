using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserManagement.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Query(x => x.Id == request.Id)
                                        .Include(x => x.UserProfile)
                                        .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException($"User was not found for Id: {request.Id}");
        }

        user.ChangeDetails
        (
            request.UserName,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName,
            request.PersonalNumber
        );

        _userRepository.Update(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}