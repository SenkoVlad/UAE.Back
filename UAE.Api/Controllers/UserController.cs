using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper.Profiles;
using UAE.Api.ViewModels.Base;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;
using UAE.Application.Validations;

namespace UAE.Api.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IUserService _userService;
    private readonly IValidationFactory _validationFactory;
    
    public UserController(IUserService userService, 
        IValidationFactory validationFactory)
    {
        _userService = userService;
        _validationFactory = validationFactory;
    }

    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register([FromBody] CreateUserModel createUserModel)
    {
        var validator = _validationFactory.GetValidator<CreateUserModel>();
        var validationResult = await validator.ValidateAsync(createUserModel);

        if (validationResult.IsValid)
        {
            var registerUserResult = await _userService.RegisterAsync(createUserModel);
            
            var apiResult = registerUserResult.ToApiResult(() => registerUserResult.Result);
            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }
    
    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginUserModel loginUserModel)
    {
        var loginResult = await _userService.LoginAsync(loginUserModel);
        var apiResult = loginResult.ToApiResult(() => loginResult.Result);

        return Ok(apiResult);
    }

    [HttpPost(nameof(LikeAnnouncement))]
    public async Task<IActionResult> LikeAnnouncement([FromBody] string announcementId)
    {
        var operationResult = await _userService.LikeAnnouncementAsync(announcementId);
        var apiResult = operationResult.ToApiResult(() => operationResult.Result);

        return Ok(apiResult);
    }
    
    [HttpPost(nameof(UnLikeAnnouncement))]
    public async Task<IActionResult> UnLikeAnnouncement([FromBody] string announcementId)
    {
        var operationResult = await _userService.UnLikeAnnouncementAsync(announcementId);
        var apiResult = operationResult.ToApiResult(() => operationResult.Result);

        return Ok(apiResult);
    }
}