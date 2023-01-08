using System.ComponentModel;

namespace UAE.Shared.Enum;

public enum FieldType
{
    [Description("Text")]
    Text,
    
    [Description("Multiselect")]
    Multiselect,
    
    [Description("Range")]
    Range
}