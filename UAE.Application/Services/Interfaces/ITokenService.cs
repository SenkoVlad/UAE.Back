using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface ITokenService
{
    bool IsUserLoggedAndTokenValid(LoginUserModel loginUserModel);

    public Task RefreshAsync();

    string CreateToken(User user);

    void AddTokenCookiesToResponse(string token, User user);
}