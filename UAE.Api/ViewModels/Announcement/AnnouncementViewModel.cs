using UAE.Application.Models.Category;
using UAE.Application.Models.Picture;
using UAE.Core.Entities;

namespace UAE.Api.ViewModels.Announcement;

public sealed record AnnouncementViewModel(
    string Id,
    string Title,
    string? Description,
    string CategoryId,
    CategoryPathModel[] CategoryPath,
    long CreatedDateTime,
    long LastUpdateDateTime,
    PictureModel[] Pictures, 
    string? Fields, 
    string? AddressToTake, 
    string? Address);
