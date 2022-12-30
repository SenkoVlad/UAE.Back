using UAE.Core.Entities;

namespace UAE.Application.Models.Category;

public sealed record CategoryWithAllChildrenModel(
    string Id,
    string Label,
    List<Field> Fields,
    string[]? Children = null);