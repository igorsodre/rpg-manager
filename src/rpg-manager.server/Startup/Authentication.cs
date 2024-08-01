using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace rpg_manager.server.Startup;

public static class Authentication
{
    public static WebApplicationBuilder AddAsymmetricAuthentication(this WebApplicationBuilder builder)
    {
        var authenticationSection = builder.Configuration.GetSection("Authentication").Get<AuthenticationSection>();
        if (authenticationSection is null)
        {
            throw new InvalidOperationException("Authentication section is missing from configuration.");
        }

        builder.Services.AddAuthentication(
                options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(
                options => {
                    using var rsa = RSA.Create();
                    rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(authenticationSection.PublicKey), out _);

                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa.ExportParameters(false)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha512 },
                        ClockSkew = TimeSpan.Zero,
                    };
                }
            );
        builder.Services.AddSingleton(authenticationSection);
        return builder;
    }
}
