using System.Globalization;
using System.Reflection;

namespace Nexus.Crypto.SDK;

public static class QueryParameterHelper
{
    public static string ToQueryString(object? obj)
    {
        if (obj == null) return string.Empty;

        var properties = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => (Key: ToCamelCase(p.Name), Value: p.GetValue(obj)))
            .Where(kv => kv.Value != null)
            .Select(kv =>
                $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(FormatValue(kv.Value!))}");

        return string.Join("&", properties);
    }

    private static string FormatValue(object value) => value switch
    {
        DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture),
        DateTimeOffset dto => dto.ToString("O", CultureInfo.InvariantCulture),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString()!
    };

    private static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
            return name;

        return char.ToLower(name[0], CultureInfo.InvariantCulture) + name[1..];
    }
}