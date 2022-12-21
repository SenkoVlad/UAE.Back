using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
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

    [HttpGet]
    public IActionResult Get()
    {
        return NotFound();
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
            return Ok(ApiResult<string>.Failure(registerUserResult.ResultMessages));
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }
    
    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login([FromBody] LoginUserModel loginUserModel)
    {
        var loginResult = await _userService.LoginAsync(loginUserModel);

        if (loginResult.IsSucceed)
        {
            return Ok(ApiResult<LoginUserResult>.Success(resultMessage: new []{loginResult.Result}, loginResult));
        }

        return Ok(ApiResult<LoginUserResult>.Failure(new[] {loginResult.Message}));
    }
}