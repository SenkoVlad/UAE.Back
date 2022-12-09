using UAE.Application.Models;
using UAE.Core.Entities;

namespace UAE.Application.Services.Interfaces;

public interface ITokenService
{
    public Task<OperationResult> RefreshAsync();

    string CreateToken(User user);

    void AddTokenCookiesToResponse(string token, User user);
}