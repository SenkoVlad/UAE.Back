using UAE.Application.Models.User;

namespace UAE.Application.Services.Interfaces;

public interface ITokenService
{
    bool IsUserLoggedAndTokenValid(LoginUserModel loginUserModel);
}