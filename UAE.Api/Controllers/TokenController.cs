using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper.Profiles;
using UAE.Api.ViewModels.Base;
using UAE.Application.Services.Interfaces;

namespace UAE.Api.Controllers;

[Authorize]
public class TokenController : ApiController
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpGet(nameof(Refresh))]
    public async Task<IActionResult> Refresh()
    {
        var refreshTokensResult = await _tokenService.RefreshAsync();
        var result = refreshTokensResult.ToApiResult(() => refreshTokensResult.ResultMessages);
        
        return Ok(result);
    }
}