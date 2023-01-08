using UAE.Application.Models;

namespace UAE.Application.Services.Interfaces;

public interface ITokenService
{
    public Task<OperationResult<string>> RefreshAsync();

    string CreateToken(Core.Entities.User user);
}