using System.Net;
using Microsoft.AspNetCore.Identity;
using OneOf.Monads;
using OneOf.Types;
using rpg_manager.database.Entities;
using rpg_manager.database.Repositories;
using rpg_manager.shared.utils.Types;

namespace rpg_manager.server.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<EfUserRepository> _logger;

    public EfUserRepository(UserManager<ApplicationUser> userManager, ILogger<EfUserRepository> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Result<ApplicationError, Option<ApplicationUser>>> FindUserByEmail(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user ?? Option<ApplicationUser>.None();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to retrieve user entry for email: {Email}", email);
            return new ApplicationError(
                $"Unable to retrieve user entry for email: {email}",
                [],
                HttpStatusCode.BadRequest
            );
        }
    }

    public async Task<Result<ApplicationError, Option<ApplicationUser>>> FindUserById(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user ?? Option<ApplicationUser>.None();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to retrieve user entry for Id: {UserId}", userId);
            return new ApplicationError(
                $"Unable to retrieve user entry for Id: {userId}",
                [],
                HttpStatusCode.BadRequest
            );
        }
    }

    public async Task<Result<ApplicationError, ApplicationUser>> CreateUser(CreateUserData userData)
    {
        var identityUser = new ApplicationUser
        {
            UserName = userData.UserName,
            Email = userData.Email,
            EmailConfirmed = true,
            TokenVersion = 1
        };

        try
        {
            var result = await _userManager.CreateAsync(identityUser);
            if (result.Succeeded)
            {
                return identityUser;
            }

            var errorMessages = result.Errors.ToDictionary(
                error => error.Code,
                key => new List<string> { key.Description }
            );

            _logger.LogError(
                "Unable to create user entry for Email: {Email}, errors: {@Errors}",
                userData.Email,
                errorMessages
            );

            return new ApplicationError(
                $"Unable to create user for email: {userData.Email}",
                errorMessages,
                HttpStatusCode.BadRequest
            );
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to create user entry for Email: {Email}", userData.Email);
            return new ApplicationError(
                $"Unable to create user entry for email: {userData.Email}",
                [],
                HttpStatusCode.BadRequest
            );
        }
    }
}
