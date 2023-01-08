namespace UAE.Application.Models.Announcement;

public sealed record AnnouncementBrowsingHistoryModel(
    Core.Entities.Announcement Announcement,
    long ViewDateTimeUtc);