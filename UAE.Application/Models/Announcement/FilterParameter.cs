using UAE.Shared.Enum;

namespace UAE.Application.Models.Announcement;

public abstract class FilterParameter<T>
{
    public string FieldName { get; set; }

    public T FieldValue { get; set; }

    public FilterCriteria FilterCriteria { get; set; }

    public void CreateParameter<T>()
    {
        
    }
}