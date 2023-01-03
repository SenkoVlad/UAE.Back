using MongoDB.Driver.Linq;
using MongoDB.Entities;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Infrastructure.Repositories.Base.Implementation;

namespace UAE.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var result = await DB.Find<User>()
            .Match(s => s.Email == email)
            .ExecuteSingleAsync();

        return result;
    }

    public async Task<bool> LikeAnnouncementAsync(string userId, string announcementId)
    {
        var result = await DB.Update<User>()
            .Match(u => u.ID == userId)
            .Modify(u => u.Push(c => c.Likes, announcementId))
            .ExecuteAsync();

        return result.ModifiedCount == 1;
    }

    public async Task<bool> UnLikeAnnouncement(string userId, string announcementId)
    {
        var result = await DB.Update<User>()
            .Match(u => u.ID == userId)
            .Modify(u => u.Pull(c => c.Likes, announcementId))
            .ExecuteAsync();
        
        return result.ModifiedCount == 1;
    }

    public async Task<bool> IsAnnouncementAlreadyLiked(string userId, string announcementId)
    {
        var result = await DB.Queryable<User>().CountAsync(c => c.ID == userId
                                                                && c.Likes.Contains(announcementId));
        return result > 0;
    }
}