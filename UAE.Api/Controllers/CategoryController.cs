﻿using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Application.Mapper;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.Base;
using UAE.Application.Models.Category;
using UAE.Application.Validation;
using UAE.Core.Entities;
using UAE.Core.Repositories;

namespace UAE.Api.Controllers;

public class CategoryController : ApiController
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryFieldsValidationService _categoryFieldsValidationService;
    
    public CategoryController(ICategoryRepository categoryRepository,
        CategoryFieldsValidationService categoryFieldsValidationService)
    {
        _categoryRepository = categoryRepository;
        _categoryFieldsValidationService = categoryFieldsValidationService;
    }

    [HttpGet(nameof(GetAll))]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var categoryModels = categories
            .Select(c => c.ToBusinessModel())
            .ToList();
        var apiResult = ApiResult<List<CategoryModel>>.Success(categoryModels);
        
        return Ok(apiResult);
    }

    [HttpPost(nameof(ValidateFields))]
    public IActionResult ValidateFields([FromBody] string[] fields, [FromQuery] string categoryId)
    {
        var result = _categoryFieldsValidationService.ValidateByCategory(fields, categoryId);
        return Ok(result);
    }
}