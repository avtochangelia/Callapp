using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<(bool success, User? user)> ValidateUserAsync(string userName, string password);
}