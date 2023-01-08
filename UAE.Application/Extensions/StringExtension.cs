using MongoDB.Bson;
using UAE.Shared.Enum;

namespace UAE.Application.Extensions;

public static class StringExtension
{
    public static BsonValue ToBsonValueByTypeName(this string value, FieldValueType valueType)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return BsonNull.Value;
        }
        
        switch (valueType)
        {
            case FieldValueType.Int32:
                return new BsonInt32(Convert.ToInt32(value));
            case FieldValueType.Decimal:
                return new BsonDecimal128(Convert.ToDecimal(value));
            default:
                return new BsonString(value);
        }
    }
}