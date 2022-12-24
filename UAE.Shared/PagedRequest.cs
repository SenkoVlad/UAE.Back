namespace UAE.Shared;

public record PagedRequest(int PageNumber, int PageSize, string? SortedBy);
