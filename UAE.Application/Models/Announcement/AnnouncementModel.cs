using MongoDB.Bson;
using UAE.Application.Models.Category;
using UAE.Application.Models.Picture;

namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementModel(
    string Id,
    string Title,
    string? Description,
    string CategoryId,
    string CurrencyId,
    decimal Price,
    CategoryPathModel[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    BsonDocument? Fields, 
    PictureModel[] Pictures,
    string? AddressToTake, 
    string? Address);