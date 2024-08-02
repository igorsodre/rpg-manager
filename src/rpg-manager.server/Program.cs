using rpg_manager.database;
using rpg_manager.server.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddJsonFile("secrets.json", true)
    .AddEnvironmentVariables();
{
    builder.AddErrorHandling().AddOpenApi();
    builder.AddDatabase(builder.Configuration.GetConnectionString("Database"));
    builder.AddAsymmetricAuthentication().AddRepositories().AddServices();
}

// Configure the HTTP request pipeline.
var app = builder.Build();
{
    app.UseOpenApi();

    app.UseHttpsRedirection();

    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseGlobalErrorHandling();
    app.MapControllers();
}

app.Run();
