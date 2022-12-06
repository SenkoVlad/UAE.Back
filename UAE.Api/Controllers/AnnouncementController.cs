using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper.Profiles;
using UAE.Api.Validations;
using UAE.Api.ViewModels.Base;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Application.Validation;
using UAE.Shared;

namespace UAE.Api.Controllers;

[Authorize]
public class AnnouncementController : ApiController
{
    private readonly IAnnouncementService _announcementService;
    private readonly CategoryFieldsValidationService _categoryFieldsValidationService;
    private readonly IValidationFactory _validationFactory;
    
    public AnnouncementController(IAnnouncementService announcementService,
        CategoryFieldsValidationService categoryFieldsValidationService, 
        IValidationFactory validationFactory)
    {
        _announcementService = announcementService;
        _categoryFieldsValidationService = categoryFieldsValidationService;
        _validationFactory = validationFactory;
    }

    [AllowAnonymous]
    [HttpPost(nameof(Search))]
    public async Task<IActionResult> Search([FromBody] SearchAnnouncementModel searchAnnouncementModel)
    {
        var validator = _validationFactory.GetValidator<SearchAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(searchAnnouncementModel);

        if (validationResult.IsValid)
        {
            var pagedResponse = await _announcementService.SearchAnnouncement(searchAnnouncementModel);
            var apiResult = ApiResult<PagedResponse<AnnouncementModel>>.Success(pagedResponse);

            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }

    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementModel createAnnouncementModel)
    {
        var validator = _validationFactory.GetValidator<CreateAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(createAnnouncementModel);
        
        var isModelValid = _categoryFieldsValidationService.ValidateByCategory(createAnnouncementModel.Fields.Keys.ToArray(),
            createAnnouncementModel.CategoryId);

        if (isModelValid)
        {
            var operationResult = await _announcementService.CreateAnnouncement(createAnnouncementModel);
            var apiResult = operationResult.ToApiResult();

            return Ok(apiResult);
        }
        else
        {
            var apiResult = ApiResult<string>.Failure(new[] {"Model is not valid"});
            return Ok(apiResult);
        }
    }

    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update([FromBody] AnnouncementModel announcementModel)
    {
        var isModelValid = _categoryFieldsValidationService.ValidateByCategory(announcementModel.Fields.Keys.ToArray(),
            announcementModel.CategoryId);

        if (isModelValid)
        {
            var operationResult = await _announcementService.UpdateAnnouncementAsync(announcementModel);
            var apiResult = operationResult.ToApiResult();

            return Ok(apiResult);
        }
        else
        {
            var apiResult = ApiResult<string>.Failure(new[] {"Model is not valid"});
            return Ok(apiResult);
        }
    }
    
    [HttpPatch(nameof(Patch))]
    public async Task<IActionResult> Patch([FromBody] PatchAnnouncementModel announcementModel)
    {
        var isModelValid = AreFieldsValid(announcementModel);

        if (!isModelValid)
        {
            return Ok(ApiResult<string>.Failure(new[] {"Model is not valid"}));
        }

        var operationResult = await _announcementService.PatchAnnouncementAsync(announcementModel);
        var apiResult = operationResult.ToApiResult();
            
        return Ok(apiResult);
    }

    private bool AreFieldsValid(PatchAnnouncementModel announcementModel)
    {
        if (announcementModel.Fields != null)
        {
            var isModelValid = _categoryFieldsValidationService.ValidateByCategory(announcementModel.Fields.Keys.ToArray(),
                announcementModel.CategoryId);

            return isModelValid;
        }

        return true;
    }
}