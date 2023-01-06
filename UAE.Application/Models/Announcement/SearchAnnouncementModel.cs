using MongoDB.Bson;
using UAE.Shared;

namespace UAE.Application.Models.Announcement;

public sealed record SearchAnnouncementModel(
    string? Description,
    List<string>? CategoryIds,
    decimal?[]? Price,
    int PageNumber,
    int PageSize,
    Dictionary<string, string[]>?  Filters,
    string SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);