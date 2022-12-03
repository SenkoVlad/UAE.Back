using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Api.Mapper;
using UAE.Application.Mapper;
using UAE.Application.Models.Announcement;
using UAE.Application.Models.Base;
using UAE.Application.Services.Interfaces;
using UAE.Shared;

namespace UAE.Api.Controllers;

[Authorize]
public class AnnouncementController : ApiController
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [AllowAnonymous]
    [HttpPost(nameof(Search))]
    public async Task<IActionResult> Search([FromBody] SearchAnnouncementModel searchAnnouncementModel)
    {
         var pagedResponse = await _announcementService.SearchAnnouncement(searchAnnouncementModel);
         var apiResult = ApiResult<PagedResponse<AnnouncementModel>>.Success(pagedResponse);

         return Ok(apiResult);
    }

    [HttpPost(nameof(Create))]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementModel createAnnouncementModel)
    {
        var operationResult = await _announcementService.CreateAnnouncement(createAnnouncementModel);
        var apiResult = ApiMapper.Mapper.Map<ApiResult<IEnumerable<string>>>(operationResult);

        return Ok(apiResult);
    }

    [HttpPatch(nameof(Update))]
    public async Task<IActionResult> Update([FromBody] AnnouncementModel announcementModel)
    {
        var operationResult = await _announcementService.UpdateAnnouncementAsync(announcementModel);
        var apiResult = ApiMapper.Mapper.Map<ApiResult<IEnumerable<string>>>(operationResult);

        return Ok(apiResult);
    }
}