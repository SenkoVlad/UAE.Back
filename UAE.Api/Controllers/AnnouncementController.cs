using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
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
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchAnnouncementModel searchAnnouncementModel)
    {
         var pagedResponse = await _announcementService.SearchAnnouncement(searchAnnouncementModel);
         var apiResult = ApiResult<PagedResponse<AnnouncementModel>>.Success(pagedResponse);

         return Ok(apiResult);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementModel createAnnouncementModel)
    {
        var operationResult = await _announcementService.CreateAnnouncement(createAnnouncementModel);
        var apiResult = ApplicationMapper.Mapper.Map<ApiResult<IEnumerable<string>>>(operationResult);

        return Ok(apiResult);
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateAnnouncementModel updateAnnouncementModel)
    {
        var operationResult = await _announcementService.UpdateAnnouncementAsync(updateAnnouncementModel);
        var apiResult = ApplicationMapper.Mapper.Map<ApiResult<IEnumerable<string>>>(operationResult);

        return Ok(apiResult);
    }
}