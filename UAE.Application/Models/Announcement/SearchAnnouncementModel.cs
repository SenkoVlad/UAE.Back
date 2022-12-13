using MongoDB.Bson;
using UAE.Shared;

namespace UAE.Application.Models.Announcement;

public sealed record SearchAnnouncementModel(
        string? Description,
        string CategoryId,
        int PageNumber,
        int PageSize,
        BsonDocument Filters,
        string SortedBy) : PagedRequest(PageNumber, PageSize, SortedBy);
