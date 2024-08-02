using OneOf.Monads;
using OneOf.Types;
using rpg_manager.database.Entities;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.database.Repositories;

public record struct CreateUserData(string Name, string Email, string UserName, string? Id = null)
{
    public string Id { get; init; } = Id ?? string.Empty;
};

public interface IUserRepository
{
    Task<Result<ApplicationError, Option<ApplicationUser>>> FindUserByEmail(string email);

    Task<Result<ApplicationError, Option<ApplicationUser>>> FindUserById(string userId);

    Task<Result<ApplicationError, ApplicationUser>> CreateUser(CreateUserData userData);
}
