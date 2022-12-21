using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record CreateAnnouncementModel(
    string Description,
    string Title,
    string CategoryId,
    BsonDocument Fields,
    string AddressToTake,
    string Address,
    List<IFormFile> Pictures
);