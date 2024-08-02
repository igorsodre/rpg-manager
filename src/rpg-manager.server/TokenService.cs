using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using rpg_manager.server.Startup;

namespace rpg_manager.server;

public class TokenService
{
    private readonly RsaSecurityKey _rsaKey;
    private readonly TimeProvider _timeProvider;
    private readonly int _tokenLifetimeInMinutes;

    public TokenService(JwtConfigOptions jwtConfigOptions, TimeProvider timeProvider)
    {
        _tokenLifetimeInMinutes = jwtConfigOptions.TokenLifetimeInMinutes;
        _timeProvider = timeProvider;
        using var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(jwtConfigOptions.PrivateKey), out _);
        var rsaParameters = rsa.ExportParameters(false);
        _rsaKey = new RsaSecurityKey(rsaParameters);
    }

    public string GenerateToken(IEnumerable<Claim> tokenAttributes)
    {
        var signingCredentials = new SigningCredentials(_rsaKey, SecurityAlgorithms.HmacSha512);
        var descriptor = new SecurityTokenDescriptor
        {
            IssuedAt = _timeProvider.GetUtcNow().UtcDateTime,
            NotBefore = _timeProvider.GetUtcNow().UtcDateTime,
            Expires = _timeProvider.GetUtcNow().AddMinutes(_tokenLifetimeInMinutes).UtcDateTime,
            Subject = new ClaimsIdentity(tokenAttributes),
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }
}
