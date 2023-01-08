namespace UAE.Api.ViewModels.Announcement;

public sealed record AnnouncementBrowsingHistoryViewModel(
    AnnouncementViewModel Announcement,
    long ViewDateTimeUtc);