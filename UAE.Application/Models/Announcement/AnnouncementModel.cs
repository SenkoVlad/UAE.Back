using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementModel(
    string Id,
    string Title,
    string? Description,
    string CategoryId,
    string[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    BsonDocument? Fields, 
    string? AddressToTake, 
    string? Address);