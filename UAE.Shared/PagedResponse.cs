namespace UAE.Shared;

public class PagedResponse<T> where T : class
{
    public long TotalCount { get; init; }
        
    public int PageCount { get; init; }

    public IReadOnlyList<T> Items { get; init; }
}