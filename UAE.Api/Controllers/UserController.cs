using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper.Profiles;
using UAE.Api.ViewModels.Base;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces.User;
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

    [HttpGet(nameof(Get))]
    public async Task<IActionResult> Get()
    {
        var operationResult = await _userService.GetWithLikes();
        var apiResult = operationResult.ToApiResult(() => operationResult.Result.ToViewModel());

        return Ok(apiResult);
    }
}