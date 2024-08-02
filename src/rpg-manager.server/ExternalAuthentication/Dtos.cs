using FluentValidation;

namespace rpg_manager.server.ExternalAuthentication;

public record ExternalAuthRequest(string Provider, string IdToken)
{
    public ExternalLoginData ToLoginData()
    {
        return new ExternalLoginData(Provider, IdToken);
    }
};

public class ExternalAuthRequestValidator : AbstractValidator<ExternalAuthRequest>
{
    public ExternalAuthRequestValidator()
    {
        RuleFor(x => x.Provider).NotEmpty().NotNull().MaximumLength(100);
        RuleFor(x => x.IdToken).NotEmpty().NotNull().MaximumLength(2000);
    }
}
