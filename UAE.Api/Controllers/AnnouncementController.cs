using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper.Profiles;
using UAE.Api.ViewModels.Announcement;
using UAE.Api.ViewModels.Base;
using UAE.Application.Mapper.Profiles;
using UAE.Application.Models.Announcement;
using UAE.Application.Services.Interfaces;
using UAE.Application.Validations;
using UAE.Core.Entities;
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
    [HttpGet(nameof(Search))]
    public async Task<IActionResult> Search([FromQuery] SearchAnnouncementViewModel searchAnnouncementViewModelModel)
    {
        var searchAnnouncementModel = searchAnnouncementViewModelModel.ToBusinessModel();
        var validator = _validationFactory.GetValidator<SearchAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(searchAnnouncementModel);

        if (validationResult.IsValid)
        {
            var pagedResponse = await _announcementService.SearchAnnouncement(searchAnnouncementModel);
            var pagedViewModelResponse = new PagedResponse<AnnouncementViewModel>(
                pagedResponse.TotalCount,
                pagedResponse.PageCount,
                pagedResponse.Items.Select(a => a.ToViewModel()).ToList());
            var apiResult = ApiResult<PagedResponse<AnnouncementViewModel>>.Success(result: pagedViewModelResponse, resultMessage: new []{ "Success" });

            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }

    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create([FromForm] CreateAnnouncementViewModel createAnnouncementViewModel)
    {
        var createAnnouncementModel = createAnnouncementViewModel.ToBusinessModel();
        var validator = _validationFactory.GetValidator<CreateAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(createAnnouncementModel);

        if (validationResult.IsValid)
        {
            var operationResult = await _announcementService.CreateAnnouncement(createAnnouncementModel);
            var apiResult = operationResult.ToApiResult(() => operationResult.Result?.ToViewModel());

            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }

    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update([FromBody] UpdateAnnouncementViewModel announcementViewModel)
    {
        var updateAnnouncementModel = announcementViewModel.ToBusinessModel();
        var validator = _validationFactory.GetValidator<UpdateAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(updateAnnouncementModel);
        
        if (validationResult.IsValid)
        {
            var operationResult = await _announcementService.UpdateAnnouncementAsync(updateAnnouncementModel);
            var apiResult = operationResult.ToApiResult(() => operationResult.Result.ToViewModel());

            return Ok(apiResult);
        }
        
        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }
    
    [HttpPatch(nameof(Patch))]
    public async Task<IActionResult> Patch([FromForm] PatchAnnouncementViewModel announcementViewModel)
    {
        var announcementModel = announcementViewModel.ToBusinessModel();
        var validator = _validationFactory.GetValidator<PatchAnnouncementModel>();
        var validationResult = await validator.ValidateAsync(announcementModel);

        if (validationResult.IsValid)
        {
            var operationResult = await _announcementService.PatchAnnouncementAsync(announcementModel);
            var apiResult = operationResult.ToApiResult(() => operationResult.Result.ToViewModel());
            
            return Ok(apiResult);
        }

        return Ok(ApiResult<string>.ValidationFailure(validationResult.Errors));
    }

    [HttpDelete(nameof(Delete))]
    public async Task<IActionResult> Delete(string id)
    {
        var operationResult = await _announcementService.DeleteAnnouncementAsync(id);
        var apiResult = operationResult.ToApiResult(() => operationResult.Result);

        return Ok(apiResult);
    }
}