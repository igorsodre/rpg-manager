using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using rpg_manager.database.Repositories;
using rpg_manager.server.ExternalAuthentication;
using rpg_manager.server.Infrastructure.Repositories;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.server.Startup;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddFluentValidationAutoValidation(options => { options.DisableDataAnnotationsValidation = true; })
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining(typeof(AssemblyMarker));

        builder.Services.AddScoped<ExternalAuthenticationService>();
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, EfUserRepository>();
        return builder;
    }

    public static WebApplicationBuilder AddErrorHandling(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails(
            options => {
                options.CustomizeProblemDetails = (context) => {
                    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                };
            }
        );
        return builder;
    }

    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.Map(
            "/error",
            (HttpContext httpContext) => {
                var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (exception is null)
                {
                    return Results.Problem();
                }

                return exception switch
                {
                    RpgManagerException appException => Results.Problem(
                        detail: appException.Message,
                        statusCode: appException.Code,
                        title: "Something went wrong"
                    ),
                    _ => Results.Problem()
                };
            }
        );
        return app;
    }
}
