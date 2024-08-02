using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace rpg_manager.database.Entities;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [MinLength(2)]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public int TokenVersion { get; set; } = 1;
}
