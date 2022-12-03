namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    DateTime CreatedDateTime,
    DateTime LastUpdateDateTime,
    Dictionary<string, object> Fields, 
    string AddressToTake, 
    string Address);