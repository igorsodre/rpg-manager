using Microsoft.AspNetCore.Mvc;
using OneOf.Monads;
using rpg_manager.server.Types;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.server;

public static class ResponseExtensions
{
    public static IActionResult ToHttpResponse<T>(this Result<ApplicationError, T> result)
    {
        return result.Match<IActionResult>(
            error => new ObjectResult(error.Value.ToProblemDetails()),
            success => new OkObjectResult(success.Value.ToApiResponse())
        );
    }

    public static ProblemDetails ToProblemDetails(this ApplicationError error) =>
        new()
        {
            Title = "One or more problems occurred",
            Detail = error.ErrorMessage,
            Extensions = new Dictionary<string, object?>
            {
                ["errors"] = error.ErrorMessages,
            },
            Status = (int)error.StatusCode,
        };
}
