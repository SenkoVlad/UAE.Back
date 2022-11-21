using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;

namespace UAE.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ApiController
{
    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }
}