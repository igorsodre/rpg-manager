using System.Net;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using OneOf.Monads;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.server.Infrastructure.LoginProviders;

// public record GoogleSettings(string ClientId, string ClientSecret);
public class GoogleSettings
{
    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }
}

public interface IGoogleLoginProvider
{
    Task<Result<ApplicationError, GoogleJsonWebSignature.Payload>> VerifyToken(string idToken);
}

public class GoogleLoginProvider : IGoogleLoginProvider
{
    private readonly GoogleJsonWebSignature.ValidationSettings _validationSettings;

    public GoogleLoginProvider(IOptions<GoogleSettings> googleSettings)
    {
        _validationSettings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new[] { googleSettings.Value.ClientId }
        };
    }

    public async Task<Result<ApplicationError, GoogleJsonWebSignature.Payload>> VerifyToken(string idToken)
    {
        try
        {
            return await GoogleJsonWebSignature.ValidateAsync(idToken, _validationSettings);
        }
        catch
        {
            return new ApplicationError(
                ErrorMessage: "Token verification failed",
                ErrorMessages: [],
                StatusCode: HttpStatusCode.Unauthorized
            );
        }
    }
}
