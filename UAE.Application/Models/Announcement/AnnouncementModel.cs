using MongoDB.Bson;
using UAE.Application.Models.Category;
using UAE.Application.Models.Picture;

namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    string CurrencyCode,
    decimal Price,
    CategoryShortModel[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    BsonDocument? Fields, 
    PictureModel[] Pictures,
    string? AddressToTake, 
    string? Address);