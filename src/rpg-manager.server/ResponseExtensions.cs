using Microsoft.AspNetCore.Mvc;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.server;

public static class ResponseExtensions
{
    public static IActionResult ToProblemResponse(this ApplicationError error) =>
        new ObjectResult(
            new ProblemDetails
            {
                Title = "One or more problems occurred",
                Detail = error.ErrorMessage,
                Extensions = new Dictionary<string, object?>
                {
                    ["errors"] = error.ErrorMessages,
                },
                Status = (int)error.StatusCode,
            }
        );
}
