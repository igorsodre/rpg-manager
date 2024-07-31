using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rpg_manager.database.Entities;

namespace rpg_manager.database;

public class Database : IdentityDbContext<ApplicationUser>
{
    public Database(DbContextOptions<Database> options) : base(options) { }
}
