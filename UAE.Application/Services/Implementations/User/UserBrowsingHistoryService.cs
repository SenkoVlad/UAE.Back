using UAE.Application.Models;
using UAE.Application.Services.Interfaces.User;
using UAE.Core.DataModels;
using UAE.Core.Repositories;

namespace UAE.Application.Services.Implementations.User;

internal sealed class UserBrowsingHistoryService : IUserBrowsingHistoryService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    
    public UserBrowsingHistoryService(IUserRepository userRepository, 
        IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<OperationResult<string>> AddAnnouncementBrowsingHistory(string announcementId)
    {
        var userId = _userService.GetCurrentUserId();
        
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult<string>(IsSucceed: false, ResultMessages: new[] {"There is not userId in cookies. History was not created"});
        }
        
        var browsingHistory = new AnnouncementBrowsingHistory(announcementId, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        var result = await _userRepository.AddAnnouncementBrowsingHistoryAsync(userId, browsingHistory);

        return new OperationResult<string>(IsSucceed: result);
    }
}