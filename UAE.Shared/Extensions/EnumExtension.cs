namespace UAE.Shared.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var member = type.GetMember(value.ToString());
        if (member is {Length: > 0})
        {
            var attributes = member[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (attributes is {Length: > 0})
            {
                return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
            }
        }
        
        return value.ToString();
    }
}