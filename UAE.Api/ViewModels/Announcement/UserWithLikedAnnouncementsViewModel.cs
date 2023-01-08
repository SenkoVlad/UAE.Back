namespace UAE.Api.ViewModels.Announcement;

public sealed record UserWithLikedAnnouncementsViewModel(
    string Email ,
    DateTime LastLoginDateTime,
    AnnouncementViewModel[] LikedAnnouncements,
    AnnouncementBrowsingHistoryViewModel[] ViewHistory);