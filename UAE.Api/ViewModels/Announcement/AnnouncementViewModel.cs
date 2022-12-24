using UAE.Application.Models.Category;

namespace UAE.Api.ViewModels.Announcement;

public sealed record AnnouncementViewModel(
    string Id,
    string Title,
    string? Description,
    string CategoryId,
    CategoryPathModel[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    string? Fields, 
    string? AddressToTake, 
    string? Address);
