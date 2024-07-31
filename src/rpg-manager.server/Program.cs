using rpg_manager.database;
using rpg_manager.server.Startup;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddOpenApi();
    builder.AddDatabase(builder.Configuration.GetConnectionString("Database")!);
}

// Configure the HTTP request pipeline.
var app = builder.Build();
{
    app.UseOpenApi();

    app.UseHttpsRedirection();
}

app.Run();
