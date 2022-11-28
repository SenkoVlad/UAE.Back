using UAE.Application.Models.User;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface ITokenService
{
    public Task<RefreshTokensResult> RefreshAsync();

    string CreateToken(User user);

    void AddTokenCookiesToResponse(string token, User user);
}