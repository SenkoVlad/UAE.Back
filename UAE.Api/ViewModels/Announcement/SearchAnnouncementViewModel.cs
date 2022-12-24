using UAE.Shared;
using UAE.Shared.Filtering.Announcement;

namespace UAE.Api.ViewModels.Announcement;

public sealed record SearchAnnouncementViewModel(
    FilterParameter<string>? Description,
    string? CategoryId,
    int PageNumber,
    string CurrencyId,
    FilterParameter<decimal>? Price,
    int PageSize,
    string? Filters,
    string? SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);
