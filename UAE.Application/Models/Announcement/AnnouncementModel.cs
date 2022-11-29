namespace UAE.Application.Models.Announcement;

public sealed record class AnnouncementModel(
    string Title,
    string Description,
    DateTime CreateDateTime);