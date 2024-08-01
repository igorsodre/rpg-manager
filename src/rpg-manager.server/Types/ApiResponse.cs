namespace rpg_manager.server.Types;

public class ApiResponse<T>
{
    public required T Data { get; set; }
}

public static class ApiResponseExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this T obj)
    {
        return new ApiResponse<T> { Data = obj };
    }
}
