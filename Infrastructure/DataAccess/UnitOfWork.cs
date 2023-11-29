using Domain.Interfaces.DataAccess;

namespace Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly EFDbContext _context;

    public UnitOfWork(EFDbContext context)
    {
        _context = context;
    }

    public async Task SaveAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        await _context.SaveChangesAsync();

        await transaction.CommitAsync();
    }
}