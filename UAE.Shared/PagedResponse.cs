namespace UAE.Shared;

public record PagedResponse<T>(
    long TotalCount, 
    int PageCount, 
    IReadOnlyList<T> Items) where T : class;
