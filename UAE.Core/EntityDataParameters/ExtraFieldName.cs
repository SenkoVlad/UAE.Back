using System.ComponentModel;

namespace UAE.Core.EntityDataParameters;

public enum ExtraFieldName
{
    [Description("Floor")]
    Floor,

    [Description("Year")]
    Year,
    
    [Description("Mileage")]
    Mileage,
    
    [Description("Max speed")]
    MaxSpeed,
    
    [Description("Brand")]
    Brand,
    
    [Description("Number of bedrooms")]
    NumberOfBedrooms,
    
    [Description("Number")]
    Number,
    
    [Description("Bathroom type")]
    BathroomType,
    
    [Description("Year of building")]
    YearOfBuilding
}