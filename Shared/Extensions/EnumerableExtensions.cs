using Shared.Interfaces;

namespace Shared.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Pagination<T>(this IEnumerable<T> source, IPaginationRequest request)
    {
        if (request == null)
        {
            return source;
        }

        request.PageSize = request.PageSize ?? 10;
        request.Page = request.Page ?? 1;

        return source.Skip(request.PageSize.Value * (request.Page.Value - 1)).Take(request.PageSize.Value);
    }

    public static IEnumerable<T> FilterByPredicate<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var filteredSource = source.Where(predicate);

        if (filteredSource == null || !filteredSource.Any())
        {
            throw new KeyNotFoundException("Record not found!");
        }

        return filteredSource;
    }
}