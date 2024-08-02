using System.Text;

namespace rpg_manager.shared.utils;

public static class StringExtensions
{
    public static string FromBase64(this string base64)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
