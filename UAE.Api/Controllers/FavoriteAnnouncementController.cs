using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper.Profiles;
using UAE.Application.Services.Interfaces.User;

namespace UAE.Api.Controllers;

[Authorize]
public class FavoriteAnnouncementController : ApiController
{
    private readonly IUserService _userService;

    
    public FavoriteAnnouncementController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost(nameof(Like))]
    public async Task<IActionResult> Like([FromBody] string announcementId)
    {
        var operationResult = await _userService.LikeAnnouncementAsync(announcementId);
        var apiResult = operationResult.ToApiResult(() => operationResult.Result);

        return Ok(apiResult);
    }
    
    [HttpPost(nameof(Dislike))]
    public async Task<IActionResult> Dislike([FromBody] string announcementId)
    {
        var operationResult = await _userService.UnLikeAnnouncementAsync(announcementId);
        var apiResult = operationResult.ToApiResult(() => operationResult.Result);

        return Ok(apiResult);
    }
}