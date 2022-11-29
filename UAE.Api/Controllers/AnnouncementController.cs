using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UAE.Api.Controllers.Base;
using UAE.Application.Models.Announcement;
using UAE.Application.Models.Base;
using UAE.Application.Services.Interfaces;
using UAE.Shared;

namespace UAE.Api.Controllers;

[Authorize]
public class AnnouncementController : ApiController
{
    private IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [AllowAnonymous]
    [HttpPost("search")]
    public async Task<IActionResult> Search([FromBody] SearchAnnouncementModel searchAnnouncementModel)
    {
         var pagedResponse = await _announcementService.SearchAnnouncement(searchAnnouncementModel);
         var apiResult = ApiResult<PagedResponse<AnnouncementModel>>.Success(pagedResponse);

         return Ok(apiResult);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementModel createAnnouncementModel)
    {
        var operationResult = await _announcementService.CreateAnnouncement(createAnnouncementModel);

        if (operationResult.IsSucceed)
        {
            var succeedResult = ApiResult<string>.Success(operationResult.ResultMessage); 
            return Ok(succeedResult);
        }

        var failedResult = ApiResult<string>.Failure(new[] {operationResult.ResultMessage});
        return Ok(failedResult);
    }
}