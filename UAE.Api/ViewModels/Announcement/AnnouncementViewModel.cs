using UAE.Application.Models.Category;
using UAE.Application.Models.Picture;

namespace UAE.Api.ViewModels.Announcement;

public sealed record AnnouncementViewModel(
    string Id,
    string Title,
    string? Description,
    string CategoryId,
    string CurrencyCode,
    decimal Price,
    CategoryShortModel[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    PictureModel[] Pictures, 
    string? Fields, 
    string? AddressToTake, 
    string? Address);
