using MongoDB.Bson;

namespace UAE.Application.Services.Validation.Interfaces;

public interface IFilterFieldsValidationService
{
    bool ValidateFilterField(IEnumerable<BsonValue> filtersParameters);
}