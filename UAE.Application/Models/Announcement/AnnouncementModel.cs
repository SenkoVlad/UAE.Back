using MongoDB.Bson;
using UAE.Application.Models.Category;

namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementModel(
    string Id,
    string Title,
    string? Description,
    string CategoryId,
    CategoryPathModel[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    BsonDocument? Fields, 
    string? AddressToTake, 
    string? Address);