using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Core.DataModels;
using UAE.Core.Repositories.Base;
using UAE.Shared.Enum;

namespace UAE.Api.Controllers;

public class CurrencyController : ApiController
{
    private readonly IRepositoryBase<Currency> _currency;

    public CurrencyController(IRepositoryBase<Currency> currency)
    {
        _currency = currency;
    }

    [HttpGet(nameof(GetCurrency))]
    public async Task<IActionResult> GetCurrency()
    {
        var currencies = await _currency.GetAllAsync();
        
        return Ok(currencies);
    }
}