namespace UAE.Application.Models.Announcement;

public sealed record CreateAnnouncementModel(
    string Description,
    string Title,
    string CategoryId,
    Dictionary<string, object> Fields,
    string AddressToTake,
    string Address
);