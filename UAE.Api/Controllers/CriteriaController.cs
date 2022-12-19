using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Shared.Enum;

namespace UAE.Api.Controllers;

public class CriteriaController : ApiController
{
    [HttpGet(nameof(GetFilterCriteria))]
    public IActionResult GetFilterCriteria()
    {
        var criteria = Enum.GetValues(typeof(FilterCriteria))
            .Cast<FilterCriteria>()
            .Select(f => f.ToString())
            .ToList();

        return Ok(criteria);
    }
}