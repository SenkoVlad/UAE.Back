using UAE.Shared;

namespace UAE.Api.ViewModels.Announcement;

public sealed record SearchAnnouncementViewModel(
    string? Description,
    string[]? CategoryIds,
    int PageNumber,
    decimal?[]? Price,
    int PageSize,
    Dictionary<string, string[]>? Filters,
    string? SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);
