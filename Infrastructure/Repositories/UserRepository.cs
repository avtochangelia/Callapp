using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Shared.Helpers;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(EFDbContext context) : base(context)
    {

    }

    public async Task<(bool success, User user)> ValidateUserAsync(string userName, string password)
    {
        var user = await Query(x => x.UserName == userName &&
                                    x.Password == HashHelper.HashPassword(password) &&
                                    x.IsActive == true)
                        .FirstOrDefaultAsync();

        if (user != null)
        {
            return (true, user);
        }

        return (false, null);
    }
}