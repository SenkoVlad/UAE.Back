using MongoDB.Bson;
using UAE.Shared;
using UAE.Shared.Filtering.Announcement;

namespace UAE.Application.Models.Announcement;

public sealed record SearchAnnouncementModel(
        FilterParameter<string>? Description,
        List<string> CategoryIds,
        FilterParameter<decimal>? Price,
        int PageNumber,
        int PageSize,
        BsonDocument Filters,
        string SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);