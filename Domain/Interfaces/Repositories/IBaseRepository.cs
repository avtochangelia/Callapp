using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> Query(Expression<Func<T, bool>>? expression = default);
    Task<T?> OfIdAsync(int id);
    Task InsertAsync(T aggregateRoot);
    void Update(T aggregateRoot);
    void Delete(T aggregateRoot);
}