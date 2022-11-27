﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
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

    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh()
    {
        await _tokenService.RefreshAsync();

        return Ok();
    }
}