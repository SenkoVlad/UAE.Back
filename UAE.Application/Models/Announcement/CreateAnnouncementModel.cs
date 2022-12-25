using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record CreateAnnouncementModel(
    string Description,
    string Title,
    string CategoryId,
    string CurrencyId,
    decimal Price,
    BsonDocument Fields,
    string AddressToTake,
    string Address,
    List<IFormFile> Pictures
);