using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Utils.Pagination;

public static class QueryablePaginationExtensions
{
    public static async Task<Page<T>> ToPageAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        int totalCount = await query.CountAsync();
        List<T> data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new Page<T>
        {
            Data = data,
            TotalCount = totalCount,
            HasPrevPage = pageNumber > 1,
            HasNextPage = pageNumber * pageSize < totalCount,
        };
    }
}
