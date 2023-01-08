using UAE.Api.ViewModels.Announcement;
using UAE.Application.Models.User;

namespace UAE.Api.Mapper.Profiles;

public static class UserMappingProfile
{
    public static UserWithLikedAnnouncementsViewModel ToViewModel(this UserWithAnnouncementsModel model)
    {
        return new UserWithLikedAnnouncementsViewModel(
            LikedAnnouncements: model.LikedAnnouncements
                .Select(a => a.ToViewModel())
                .ToArray(),
            Email: model.Email,
            LastLoginDateTime: model.LastLoginDateTime,
            ViewHistory: model.ViewHistory
                .Select(v => v.ToViewModel())
                .ToArray());
    }
}