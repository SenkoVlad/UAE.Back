using UAE.Api.ViewModels.Announcement;
using UAE.Application.Models.User;

namespace UAE.Api.Mapper.Profiles;

public static class UserMappingProfile
{
    public static UserWithLikedAnnouncementsViewModel ToViewModel(this UserWithLikedAnnouncementsModel model)
    {
        return new UserWithLikedAnnouncementsViewModel(
            LikedAnnouncements: model.LikedAnnouncements
                .Select(a => a.ToViewModel())
                .ToArray(),
            Email: model.Email,
            LastLoginDateTime: model.LastLoginDateTime);
    }
}