using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record UpdateAnnouncementModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    string CurrencyCode,
    decimal Price,
    BsonDocument Fields, 
    string AddressToTake, 
    string Address,
    long CreatedDateTime,
    List<IFormFile> Pictures);
