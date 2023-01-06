using System.ComponentModel;

namespace UAE.Core.EntityDataParameters;

public enum FieldType
{
    [Description("Text")]
    Text,
    
    [Description("Multiselect")]
    Multiselect,
    
    [Description("Range")]
    Range
}