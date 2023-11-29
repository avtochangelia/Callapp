using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query(Expression<Func<T, bool>> expression = default)
    {
        return expression == null ? _context.Set<T>().AsQueryable() : _context.Set<T>().Where(expression);
    }

    public async Task<T> OfIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual async Task InsertAsync(T aggregateRoot)
    {
        await _context.Set<T>().AddAsync(aggregateRoot);
    }

    public virtual void Update(T aggregateRoot)
    {
        _context.Entry(aggregateRoot).State = EntityState.Modified;
    }

    public virtual void Delete(T aggregateRoot)
    {
        _context.Set<T>().Remove(aggregateRoot);
    }
}