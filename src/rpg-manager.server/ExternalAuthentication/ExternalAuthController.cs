using Microsoft.AspNetCore.Mvc;
using rpg_manager.server.Types;

namespace rpg_manager.server.ExternalAuthentication;

[ApiController]
[Route("/api/auth")]
public class ExternalAuthController : ControllerBase
{
    private readonly ExternalAuthenticationService _externalAuthenticationService;

    public ExternalAuthController(ExternalAuthenticationService externalAuthenticationService)
    {
        _externalAuthenticationService = externalAuthenticationService;
    }

    [HttpPost("external")]
    [ProducesResponseType(typeof(ApiResponse<LoginResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExternalLogin(ExternalAuthRequest request, CancellationToken cancellationToken)
    {
        var result = await _externalAuthenticationService.Login(request.ToLoginData(), cancellationToken);
        return result.Match<IActionResult>(
            error => error.Value.ToProblemResponse(),
            success => Ok(success.Value.ToApiResponse())
        );
    }
}
