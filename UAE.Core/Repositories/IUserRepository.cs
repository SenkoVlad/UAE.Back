using UAE.Core.DataModels;
using UAE.Core.Entities;
using UAE.Core.Repositories.Base;

namespace UAE.Core.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmailAsync(string email);

    Task<bool> LikeAnnouncementAsync(string userId, string announcementId);
    
    Task<bool> UnLikeAnnouncementAsync(string userId, string announcementId);

    Task<bool> AddAnnouncementBrowsingHistoryAsync(string userId, AnnouncementBrowsingHistory browsingHistory);
}