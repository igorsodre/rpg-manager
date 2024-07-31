using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using rpg_manager.database.Entities;

namespace rpg_manager.database;

public static class DatabaseExtensions
{
    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder, string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string cannot be null or empty");
        }

        builder.Services.AddDbContext<Database>(
            options => {
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(Database).Assembly.FullName));
            }
        );
        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<Database>();

        return builder;
    }
}
