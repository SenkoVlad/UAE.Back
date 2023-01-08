using UAE.Application.Models;
using UAE.Application.Models.User;

namespace UAE.Application.Services.Interfaces.User;

public interface IUserService
{
    Task<OperationResult<Core.Entities.User>> RegisterAsync(CreateUserModel createUserModel);

    Task<OperationResult<string>> LoginAsync(LoginUserModel loginUserModel);
    
    string? GetCurrentUserId();
    
    Task<OperationResult<string>> LikeAnnouncementAsync(string announcementId);
    
    Task<OperationResult<string>> UnLikeAnnouncementAsync(string announcementId);
    
    Task<OperationResult<UserWithAnnouncementsModel>> GetWithLikes();
}