using UAE.Shared;

namespace UAE.Api.ViewModels.Announcement;

public sealed record SearchAnnouncementViewModel(
    string? Description,
    string CategoryId,
    int PageNumber,
    int PageSize,
    string? Filters,
    string SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);
