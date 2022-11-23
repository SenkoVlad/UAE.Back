using System;

namespace UAE.Application.Models.Order;

public sealed class AnnouncementModel
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
}