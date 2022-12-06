using System.Text.Json;

namespace UAE.Application.Extensions;

public static class DictionaryExtension
{
    public static Dictionary<string, object> ToDictionaryWithCheckingForValueKind(
        this Dictionary<string, object> objects)
    {
        var destDict = new Dictionary<string, object>();

        foreach (var key in objects.Keys)
        {
            var value = objects[key];
            var jsonValueType = value is JsonElement element
                ? element
                : default;

            switch (jsonValueType.ValueKind.ToString())
            {
                case "Number" : destDict.Add(key, jsonValueType.GetInt32());
                    break;
                case "String" : destDict.Add(key, jsonValueType.GetString()!);
                    break;
                default:
                    destDict.Add(key, value);
                    break;
            }
        }

        return destDict;
    }
}