using UAE.Core.Entities;

namespace UAE.Application.Models.Category;

public sealed record CategoryFlatModel(
    string Id,
    string Label,
    List<Field> Fields);