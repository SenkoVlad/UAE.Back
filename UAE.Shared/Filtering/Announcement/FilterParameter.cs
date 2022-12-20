using UAE.Shared.Enum;

namespace UAE.Shared.Filtering.Announcement;

public sealed record FilterParameter<T>(
    T FieldValue,
    FilterCriteria FilterCriteria 
);