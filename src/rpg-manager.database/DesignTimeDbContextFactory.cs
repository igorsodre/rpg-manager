using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace rpg_manager.database;

internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Database>
{
    public Database CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<Database>();
        builder.UseNpgsql(MigrationsConfig.GetConnectionString());
        return new Database(builder.Options);
    }
}

public static class MigrationsConfig
{
    public static string GetConnectionString()
    {
        var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var filePath = Path.Combine(buildDir!, "databasesettings.json");
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile(filePath, optional: true)
                   .AddEnvironmentVariables()
                   .Build()
                   .GetConnectionString("Database") ??
               throw new Exception("Database Connection String Not Found");
    }
}
