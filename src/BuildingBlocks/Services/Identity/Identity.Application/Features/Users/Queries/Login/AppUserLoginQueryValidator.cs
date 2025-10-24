using FluentValidation;

namespace Identity.Application.Features.Users.Queries.Login;

public class AppUserLoginQueryValidator : AbstractValidator<AppUserLoginQuery>
{
    public AppUserLoginQueryValidator()
    {
        RuleFor(x => x.UserNameOrEmail)
            .NotEmpty().WithMessage("Username or email is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}