using MongoDB.Bson;

namespace UAE.Application.Extensions;

public static class StringExtension
{
    public static BsonValue ToBsonValueByTypeName(this string value, string typeName)
    {
        switch (typeName)
        {
            case "Int32":
                return new BsonInt32(Convert.ToInt32(value));
            case "Decimal":
                return new BsonDecimal128(Convert.ToDecimal(value));
            default:
                return new BsonString(value);
        }
    }
}