using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories;

public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(EFDbContext context) : base(context)
    {

    }
}