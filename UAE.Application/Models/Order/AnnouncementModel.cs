namespace UAE.Application.Models.Order;

public sealed record class AnnouncementModel(
    string Title,
    string Description,
    DateTime CreateDateTime);