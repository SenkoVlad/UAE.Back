using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Application.Models.Base;
using UAE.Application.Models.Order;
using UAE.Application.Services.Interfaces;
using UAE.Shared;

namespace UAE.Api.Controllers;

public class AnnouncementController : ApiController
{
    private IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchAnnouncementModel searchAnnouncementModel)
    {
         var pagedResponse = await _announcementService.SearchAnnouncement(searchAnnouncementModel);
         var apiResult = ApiResult<PagedResponse<AnnouncementModel>>.Success(pagedResponse);

         return Ok(apiResult);
    }
}