using System.Net;
using OneOf.Monads;
using OneOf.Types;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.server.ExternalAuthentication;

public record LoginResult(string AccessToken, string RefreshToken);

public record LoginData(string Provider, string IdToken);

public class ExternalAuthenticationService
{
    public async Task<Result<ApplicationError, LoginResult>> Login(
        LoginData loginData,
        CancellationToken cancellationToken = default
    )
    {
        await Task.Delay(10);
        return new ApplicationError(
            ErrorMessage: "This feature is not implemented",
            ErrorMessages: [],
            StatusCode: HttpStatusCode.InternalServerError
        );
    }
}
