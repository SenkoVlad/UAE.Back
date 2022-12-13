using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record UpdateAnnouncementModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    BsonDocument Fields, 
    string AddressToTake, 
    string Address,
    long CreatedDateTime);
