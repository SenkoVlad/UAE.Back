using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record UpdateAnnouncementModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    string CurrencyId,
    decimal Price,
    BsonDocument Fields, 
    string AddressToTake, 
    string Address,
    long CreatedDateTime);
