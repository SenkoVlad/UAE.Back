using System.Text.Json;
using MongoDB.Bson;
using UAE.Application.Models.Category;
using UAE.Core.Entities;
using UAE.Shared.Extensions;

namespace UAE.Application.Mapper.Profiles;

public static class FieldMappingProfile
{
    public static FieldModel ToBusinessModel(this Field field) =>
        new(field.Name,
            (int)field.Type,
            field.PossibleValues != null
                ? JsonSerializer.Deserialize<object[]>(field.PossibleValues?.ToJson()).Select(x => x.ToString()).ToArray()
                : Array.Empty<string>());
}
