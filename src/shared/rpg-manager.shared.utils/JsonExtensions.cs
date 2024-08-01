using System.Text.Json;
using OneOf.Monads;
using OneOf.Types;

namespace rpg_manager.shared.utils;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static string ToJsonString<T>(this T? obj, JsonSerializerOptions? options)
    {
        return obj is null ? string.Empty : JsonSerializer.Serialize(obj, options ?? Options);
    }

    public static Option<T> FromJsonString<T>(this string? jsonString, JsonSerializerOptions? options)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return Option<T>.None();
        }

        try
        {
            var result = JsonSerializer.Deserialize<T>(jsonString, options ?? Options);
            return result ?? Option<T>.None();
        }
        catch { }

        return Option<T>.None();
    }
}
