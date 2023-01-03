using MongoDB.Bson;
using UAE.Application.Services.Validation.Interfaces;

namespace UAE.Application.Services.Validation.Implementation;

public class FilterFieldsValidationService : IFilterFieldsValidationService
{
    private readonly List<string> _parametersNames = new() {"fieldValue", "fieldCriteria"};

    public bool ValidateFilterField(IEnumerable<BsonValue> filtersParameters)
    {
        var parameterNamesToCheck = filtersParameters
            .SelectMany(filterParameters => filterParameters.AsBsonDocument.Names)
            .Distinct()
            .ToList();

        var marchedParametersCount = parameterNamesToCheck.Intersect(_parametersNames, StringComparer.OrdinalIgnoreCase);
        return marchedParametersCount.Count() == parameterNamesToCheck.Count;
    }
}