using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Core.EntityDataParameters;
using UAE.Shared.Enum;

namespace UAE.Api.Controllers;

public class CriteriaController : ApiController
{
    [HttpGet(nameof(GetAllFilterCriteria))]
    public IActionResult GetAllFilterCriteria()
    {
        var criteria = Enum.GetValues(typeof(FilterCriteria))
            .Cast<FilterCriteria>()
            .Select(f => f.ToString())
            .ToList();

        return Ok(criteria);
    }
    
    [HttpGet(nameof(GetAllFieldType))]
    public IActionResult GetAllFieldType()
    {
        var fieldTypes = Enum.GetValues(typeof(FieldType))
            .Cast<FieldType>()
            .Select(f => f.ToString())
            .ToList();

        return Ok(fieldTypes);
    }
    
    [HttpGet(nameof(GetAllFieldValueType))]
    public IActionResult GetAllFieldValueType()
    {
        var fieldTypes = Enum.GetValues(typeof(FieldValueType))
            .Cast<FieldValueType>()
            .Select(f => f.ToString())
            .ToList();

        return Ok(fieldTypes);
    }
}