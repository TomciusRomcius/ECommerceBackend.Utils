namespace ECommerceBackend.Utils.Pagination;

public class Page<T>
{
    public required List<T> Data { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPrevPage { get; init; }
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
}
