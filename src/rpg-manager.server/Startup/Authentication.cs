using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using rpg_manager.server.Infrastructure.LoginProviders;
using rpg_manager.server.Types;
using rpg_manager.shared.utils;

namespace rpg_manager.server.Startup;

public static class Authentication
{
    public static WebApplicationBuilder AddAsymmetricAuthentication(this WebApplicationBuilder builder)
    {
        var (accessTokenOptions, refreshTokenOptions) = CollectJwtTokenKeys(builder);
        builder.Services.Configure<GoogleSettings>(
            builder.Configuration.GetSection("Authentication").GetSection("Google")
        );

        AddJwtBearerTokenAuthScheme(builder, accessTokenOptions);

        builder.Services.AddKeyedSingleton<TokenService>(
            Constants.Jwt.TokenServiceForAccessJwt,
            (serviceProvider, _) => new TokenService(
                accessTokenOptions,
                serviceProvider.GetRequiredService<TimeProvider>()
            )
        );
        builder.Services.AddKeyedSingleton<TokenService>(
            Constants.Jwt.TokenServiceForRefreshJwt,
            (serviceProvider, _) => new TokenService(
                refreshTokenOptions,
                serviceProvider.GetRequiredService<TimeProvider>()
            )
        );

        builder.Services.AddScoped<IGoogleLoginProvider, GoogleLoginProvider>();
        return builder;
    }

    private static (JwtConfigOptions accessTokenOptions, JwtConfigOptions refreshTokenOptions) CollectJwtTokenKeys(
        WebApplicationBuilder builder
    )
    {
        var accessTokenOptions = builder.Configuration.GetSection("Authentication")
            .GetSection("AccessTokenOptions")
            .Get<JwtConfigOptions>();
        if (accessTokenOptions is null ||
            string.IsNullOrEmpty(accessTokenOptions.PrivateKey) ||
            string.IsNullOrEmpty(accessTokenOptions.PrivateKey))
        {
            throw new InvalidOperationException("AccessTokenOptions section is missing from configuration.");
        }

        var refreshTokenOptions = builder.Configuration.GetSection("Authentication")
            .GetSection("RefreshTokenOptions")
            .Get<JwtConfigOptions>();
        if (refreshTokenOptions is null ||
            string.IsNullOrEmpty(refreshTokenOptions.PrivateKey) ||
            string.IsNullOrEmpty(refreshTokenOptions.PrivateKey))
        {
            throw new InvalidOperationException("RefreshTokenOptions section is missing from configuration.");
        }

        return (accessTokenOptions, refreshTokenOptions);
    }

    private static void AddJwtBearerTokenAuthScheme(WebApplicationBuilder builder, JwtConfigOptions accessTokenOptions)
    {
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
                    rsa.ImportFromPem(accessTokenOptions.PrivateKey.FromBase64());

                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa.ExportParameters(false)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha512 },
                        ClockSkew = TimeSpan.Zero,
                    };
                }
            );
    }
}
