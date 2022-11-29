namespace UAE.Application.Models.Announcement;

public sealed record CreateAnnouncementModel(
    string Description,
    string Title,
    string CategoryId,
    Dictionary<string, string> Parameters,
    string AddressToTake,
    string Address
);