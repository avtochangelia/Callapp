using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.UserManagement.Commands.DeactivateUser;

public class DeactivateUserHandler : IRequestHandler<DeactivateUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeactivateUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OfIdAsync(request.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        user.DeactivateUser();

        _userRepository.Update(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}