using UAE.Shared;

namespace UAE.Application.Models.Order;

public class SearchAnnouncementModel : PagedRequest
{
    public string Description { get; set; }

    public string CategoryId { get; set; }
}