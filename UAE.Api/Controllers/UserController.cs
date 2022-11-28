using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Application.Models.Base;
using UAE.Application.Models.User;
using UAE.Application.Services.Interfaces;

namespace UAE.Api.Controllers;

[Authorize]
public class UserController : ApiController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return NotFound();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserModel createUserModel)
    {
        await _userService.RegisterAsync(createUserModel);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
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