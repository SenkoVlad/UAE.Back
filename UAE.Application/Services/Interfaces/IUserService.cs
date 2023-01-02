using UAE.Application.Models;
using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface IUserService
{
    Task<OperationResult<User>> RegisterAsync(CreateUserModel createUserModel);

    Task<OperationResult<string>> LoginAsync(LoginUserModel loginUserModel);
    
    string GetCurrentUserId();
    
    Task<OperationResult<string>> LikeAnnouncementAsync(string announcementId);
    
    Task<OperationResult<string>> UnLikeAnnouncementAsync(string announcementId);
}