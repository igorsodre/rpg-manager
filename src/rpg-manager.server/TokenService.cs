using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using rpg_manager.server.Startup;
using rpg_manager.shared.utils;

namespace rpg_manager.server;

public class TokenService
{
    private readonly RsaSecurityKey _rsaKey;
    private readonly TimeProvider _timeProvider;
    private readonly int _tokenLifetimeInMinutes;
    private readonly JwtSecurityTokenHandler _securityTokenHandler = new();

    public TokenService(JwtConfigOptions jwtConfigOptions, TimeProvider timeProvider)
    {
        _tokenLifetimeInMinutes = jwtConfigOptions.TokenLifetimeInMinutes;
        _timeProvider = timeProvider;
        using var rsa = RSA.Create();
        rsa.ImportFromPem(jwtConfigOptions.PrivateKey.FromBase64());
        var rsaParameters = rsa.ExportParameters(true);
        _rsaKey = new RsaSecurityKey(rsaParameters);
    }

    public string GenerateToken(IEnumerable<Claim> tokenAttributes)
    {
        var signingCredentials = new SigningCredentials(_rsaKey, SecurityAlgorithms.RsaSha512);
        var descriptor = new SecurityTokenDescriptor
        {
            IssuedAt = _timeProvider.GetUtcNow().UtcDateTime,
            NotBefore = _timeProvider.GetUtcNow().UtcDateTime,
            Expires = _timeProvider.GetUtcNow().AddMinutes(_tokenLifetimeInMinutes).UtcDateTime,
            Subject = new ClaimsIdentity(tokenAttributes),
            SigningCredentials = signingCredentials
        };

        var token = _securityTokenHandler.CreateToken(descriptor);

        return _securityTokenHandler.WriteToken(token);
    }
}
