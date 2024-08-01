using rpg_manager.database;
using rpg_manager.server.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddJsonFile("secretsettings.json", true)
    .AddEnvironmentVariables();
{
    builder.AddOpenApi();
    builder.AddDatabase(builder.Configuration.GetConnectionString("Database")!);
}

// Configure the HTTP request pipeline.
var app = builder.Build();
{
    app.UseOpenApi();

    app.UseHttpsRedirection();

    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
}

app.Run();
