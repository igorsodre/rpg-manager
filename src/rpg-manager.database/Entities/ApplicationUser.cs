using Microsoft.AspNetCore.Identity;

namespace rpg_manager.database.Entities;

public class ApplicationUser : IdentityUser
{
    public int TokenVersion { get; set; } = 1;
}
