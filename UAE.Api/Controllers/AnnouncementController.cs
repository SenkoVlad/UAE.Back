using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Extensions;
using UAE.Api.Mapper.Profiles;
using UAE.Api.ViewModels.Base;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Application.Validations;
using UAE.Shared;

namespace UAE.Api.Controllers;

[Authorize]
public class AnnouncementController : ApiController
{
    private readonly IAnnouncementService _announcementService;
    private readonly IValidationFactory _validationFactory;
    
    public AnnouncementController(IAnnouncementService announcementService,
        IValidationFactory validationFactory)
    {
        _announcementService = announcementService;
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

        if (validationResult.IsValid)
        {
            var operationResult = await _announcementService.CreateAnnouncement(createAnnouncementModel);
            var apiResult = operationResult.ToApiResult();

            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }

    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update([FromBody] AnnouncementModel announcementModel)
    {
        var validator = _validationFactory.GetValidator<AnnouncementModel>();
        var validationResult = await validator.ValidateAsync(announcementModel);
        
        if (validationResult.IsValid)
        {
            var operationResult = await _announcementService.UpdateAnnouncementAsync(announcementModel);
            var apiResult = operationResult.ToApiResult();

            return Ok(apiResult);
        }
        
        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }
    
    [HttpPatch(nameof(Patch))]
    public async Task<IActionResult> Patch([FromBody] PatchAnnouncementModel announcementModel)
    {
        var validator = _validationFactory.GetValidator<PatchAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(announcementModel);

        if (validationResult.IsValid)
        {
            var operationResult = await _announcementService.PatchAnnouncementAsync(announcementModel);
            var apiResult = operationResult.ToApiResult();
            
            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }

    [HttpDelete()]
    public async Task<IActionResult> Delete(string id)
    {
        var operationResult = await _announcementService.DeleteAnnouncementAsync(id);
        var apiResult = operationResult.ToApiResult();

        return Ok(apiResult);
    }
}