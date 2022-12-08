namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementModel(
    string Id,
    string Title,
    string Description,
    string CategoryId,
    long CreatedDateTime,
    long LastUpdateDateTime,
    Dictionary<string, object> Fields, 
    string AddressToTake, 
    string Address);