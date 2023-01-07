using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class FieldMappingProfile
{
    public static FieldModel ToBusinessModel(this Field field) =>
        new(field.Name,
            (int)field.Type,
            field.PossibleValues ?? Array.Empty<string>());
}
