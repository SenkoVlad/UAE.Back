using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Application.Mapper;
using UAE.Application.Models.Base;
using UAE.Application.Models.Category;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Api.Controllers;

public class CategoryController : ApiController
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoryModels = ApplicationMapper.Mapper.Map<List<CategoryModel>>(categories);
        var apiResult = ApiResult<List<CategoryModel>>.Success(categoryModels);
        
        return Ok(apiResult);
    }
}