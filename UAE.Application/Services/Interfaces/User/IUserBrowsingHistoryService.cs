using UAE.Application.Models;

namespace UAE.Application.Services.Interfaces.User;

public interface IUserBrowsingHistoryService
{
    Task<OperationResult<string>> AddAnnouncementBrowsingHistory(string announcementId);
}