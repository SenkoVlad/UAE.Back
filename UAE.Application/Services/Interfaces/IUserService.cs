using UAE.Application.Models;
using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface IUserService
{
    Task<OperationResult<User>> RegisterAsync(CreateUserModel createUserModel);

    Task<LoginUserResult> LoginAsync(LoginUserModel loginUserModel);
    
    string GetCurrentUserId();
}