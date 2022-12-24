using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace UAE.Application.Models.Announcement;

public sealed record PatchAnnouncementModel(
    string Id,
    string? Title,
    string? Description,
    string? CurrencyId,
    decimal? Price,
    string? CategoryId,
    BsonDocument? Fields, 
    string? AddressToTake, 
    string? Address,
    List<IFormFile>? Pictures);