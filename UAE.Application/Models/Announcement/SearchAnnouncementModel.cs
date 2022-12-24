using MongoDB.Bson;
using UAE.Shared;
using UAE.Shared.Filtering.Announcement;

namespace UAE.Application.Models.Announcement;

public sealed record SearchAnnouncementModel(
        FilterParameter<string>? Description,
        string? CategoryId,
        int PageNumber,
        int PageSize,
        BsonDocument Filters,
        string SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);