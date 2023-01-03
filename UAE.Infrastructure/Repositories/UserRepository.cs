using MongoDB.Entities;
using UAE.Core.DataModels;
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
        await UnLikeAnnouncementAsync(userId, announcementId);
        
        var result = await DB.Update<User>()
            .MatchID(userId)
            .Modify(u => u.Push(c => c.Likes, announcementId))
            .ExecuteAsync();

        return result.ModifiedCount == 1;
    }

    public async Task<bool> UnLikeAnnouncementAsync(string userId, string announcementId)
    {
        var result = await DB.Update<User>()
            .MatchID(userId)
            .Modify(u => u.Pull(c => c.Likes, announcementId))
            .ExecuteAsync();
        
        return result.ModifiedCount == 1;
    }

    public async Task<bool> AddAnnouncementBrowsingHistoryAsync(string userId, AnnouncementBrowsingHistory browsingHistory)
    {
        await DB.Update<User>()
            .MatchID(userId)
            .Modify(u => u.PullFilter(p => p.BrowsingHistories, 
                h => h.AnnouncementId == browsingHistory.AnnouncementId))
            .ExecuteAsync();
        
        var result = await DB.Update<User>()
            .MatchID(userId)
            .Modify(u => u.Push(c => c.BrowsingHistories, browsingHistory))
            .ExecuteAsync();

        return result.ModifiedCount == 1;
    }
}