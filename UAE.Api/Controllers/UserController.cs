using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Core.Entities;
using UAE.Core.Repositories;
using UAE.Core.Repositories.Base;

namespace UAE.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ApiController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return NotFound();
    }
}