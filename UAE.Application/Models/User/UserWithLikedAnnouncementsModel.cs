namespace UAE.Application.Models.User;

public sealed record UserWithLikedAnnouncementsModel(
    string Email ,
    DateTime LastLoginDateTime,
    List<Core.Entities.Announcement> LikedAnnouncements
);