using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using OneOf.Monads;
using rpg_manager.database.Entities;
using rpg_manager.database.Repositories;
using rpg_manager.server.Infrastructure.LoginProviders;
using rpg_manager.shared.utils.Types;
using Constants = rpg_manager.server.Types.Constants;

namespace rpg_manager.server.ExternalAuthentication;

public record LoginResult(string AccessToken, string RefreshToken);

public record ExternalLoginData(string Provider, string IdToken);

public class ExternalAuthenticationService
{
    private readonly IGoogleLoginProvider _googleLoginProvider;
    private readonly IUserRepository _userRepository;
    private readonly TokenService _accessTokenService;
    private readonly TokenService _refreshTokenService;

    public ExternalAuthenticationService(
        IGoogleLoginProvider googleLoginProvider,
        IUserRepository userRepository,
        [FromKeyedServices(Constants.Jwt.TokenServiceForAccessJwt)] TokenService accessTokenService,
        [FromKeyedServices(Constants.Jwt.TokenServiceForRefreshJwt)] TokenService refreshTokenService
    )
    {
        _googleLoginProvider = googleLoginProvider;
        _userRepository = userRepository;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<Result<ApplicationError, LoginResult>> Login(
        ExternalLoginData externalLoginData,
        CancellationToken cancellationToken = default
    )
    {
        // Verify id token is valid
        var result = await _googleLoginProvider.VerifyToken(externalLoginData.IdToken);
        if (result.IsError())
        {
            return result.ErrorValue();
        }

        var tokenPayload = result.SuccessValue();

        // Check if user exists
        var userResult = await _userRepository.FindUserByEmail(tokenPayload.Email);
        if (userResult.IsError())
        {
            return userResult.ErrorValue();
        }

        // if user does not exist, creates it
        ApplicationUser user;
        if (userResult.SuccessValue().IsSome())
        {
            user = userResult.SuccessValue().Value();
        }
        else
        {
            var userCreateResult = await _userRepository.CreateUser(
                new CreateUserData(Name: tokenPayload.Name, Email: tokenPayload.Email, UserName: tokenPayload.Email)
            );
            if (userCreateResult.IsError())
            {
                return userCreateResult.ErrorValue();
            }

            user = userCreateResult.SuccessValue();
        }

        return GenerateLoginResult(user);
    }

    private LoginResult GenerateLoginResult(ApplicationUser user)
    {
        return new LoginResult(
            _accessTokenService.GenerateToken(
                new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Email, user.Email!),
                    new(Constants.TokenClaims.Id, user.Id),
                }
            ),
            _refreshTokenService.GenerateToken(
                new List<Claim>
                {
                    new(Constants.TokenClaims.Id, user.Id),
                    new(Constants.TokenClaims.TokenVersion, user.TokenVersion.ToString())
                }
            )
        );
    }
}
