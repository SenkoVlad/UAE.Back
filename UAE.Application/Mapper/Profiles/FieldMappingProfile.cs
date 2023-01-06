using System.Text.Json;
using MongoDB.Bson;
using UAE.Application.Models.Category;
using UAE.Core.Entities;

namespace UAE.Application.Mapper.Profiles;

public static class FieldMappingProfile
{
    public static FieldModel ToBusinessModel(this Field field) =>
        new(field.Name,
            field.FieldType,
            field.PossibleValues != null
                ? JsonSerializer.Deserialize<object[]>(field.PossibleValues?.ToJson())
                : Array.Empty<object>());
}
