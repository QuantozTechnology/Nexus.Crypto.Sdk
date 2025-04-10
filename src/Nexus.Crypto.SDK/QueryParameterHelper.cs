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
            .Where(p => p.GetValue(obj) != null)
            .Select(p =>
                $"{Uri.EscapeDataString(ToCamelCase(p.Name))}={Uri.EscapeDataString(p.GetValue(obj)?.ToString()!)}");

        return string.Join("&", properties);
    }

    private static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
            return name;

        return char.ToLower(name[0], CultureInfo.InvariantCulture) + name[1..];
    }
}