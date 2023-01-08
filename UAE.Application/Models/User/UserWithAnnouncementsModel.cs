using UAE.Application.Models.Announcement;

namespace UAE.Application.Models.User;

public sealed record UserWithAnnouncementsModel(string Email,
    DateTime LastLoginDateTime,
    List<Core.Entities.Announcement> LikedAnnouncements,
    List<AnnouncementBrowsingHistoryModel> ViewHistory);