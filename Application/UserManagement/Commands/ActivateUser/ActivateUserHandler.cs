using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UserManagement.Commands.ActivateUser;

public class ActivateUserHandler : IRequestHandler<ActivateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ActivateUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OfIdAsync(request.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        user.ActivateUser();

        _userRepository.Update(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}