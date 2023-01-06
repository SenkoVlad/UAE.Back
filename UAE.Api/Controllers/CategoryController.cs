using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.ViewModels.Base;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.Category;
using UAE.Application.Services.Interfaces;
using UAE.Application.Services.Validation.Implementation;
using UAE.Core.Repositories;

namespace UAE.Api.Controllers;

public class CategoryController : ApiController
{
    private readonly CategoryFieldsValidationService _categoryFieldsValidationService;
    private readonly ICategoryInMemory _categoryInMemory;
    
    public CategoryController(
        CategoryFieldsValidationService categoryFieldsValidationService,
        ICategoryInMemory categoryInMemory)
    {
        _categoryFieldsValidationService = categoryFieldsValidationService;
        _categoryInMemory = categoryInMemory;
    }

    [HttpGet(nameof(GetAll))]
    public IActionResult GetAll()
    {
        var categories = _categoryInMemory.Data;
        var categoryModels = categories
            .Select(c => c.ToBusinessModel())
            .ToList();
        var apiResult = ApiResult<List<CategoryModel>>.Success(new []{"Success"}, categoryModels);
        
        return Ok(apiResult);
    }

    [HttpPost(nameof(ValidateFields))]
    public IActionResult ValidateFields([FromBody] string[] fields, [FromQuery] string categoryId)
    {
        var result = _categoryFieldsValidationService.DoesFieldExistInAllCategories(fields,  new []{ categoryId });
        return Ok(result);
    }
}