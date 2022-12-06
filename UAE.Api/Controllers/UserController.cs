using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Validations;
using UAE.Api.ViewModels.Base;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;

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
            await _userService.RegisterAsync(createUserModel);
            return Ok();
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
            return Ok(ApiResult<string>.Success(loginResult.Result));
        }

        return Ok(ApiResult<string>.Failure(new[] {loginResult.Message}));
    }
}