using UAE.Shared;

namespace UAE.Application.Models.Announcement;

public sealed record SearchAnnouncementModel(
        string? Description,
        string CategoryId,
        int PageNumber,
        int PageSize,
        Dictionary<string, object>? Fields,
        string SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);
