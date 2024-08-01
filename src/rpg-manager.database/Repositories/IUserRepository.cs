using OneOf.Monads;
using OneOf.Types;
using rpg_manager.database.Entities;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.database.Repositories;

public record struct CreateUserData(string Id, string Name, string Email, string UserName);

public interface IUserRepository
{
    Task<Result<ApplicationError, Option<ApplicationUser>>> FindUserByEmail(string email);

    Task<Result<ApplicationError, Option<ApplicationUser>>> FindUserById(string userId);

    Task<Result<ApplicationError, Success>> CreateUser(CreateUserData userData);
}
