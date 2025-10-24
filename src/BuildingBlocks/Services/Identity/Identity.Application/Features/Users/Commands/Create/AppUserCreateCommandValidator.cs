using FluentValidation;
using Identity.Application.Constant;

namespace Identity.Application.Features.Users.Commands.Create;

public class AppUserCreateCommandValidator : AbstractValidator<AppUserCreateCommand>
{
    public AppUserCreateCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("Username cannot be null")
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters");

        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("First name cannot be null")
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("Last name cannot be null")
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email cannot be null")
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password cannot be null")
            .NotEmpty().WithMessage("Password is required")
            .Matches(ValidationConstants.PasswordRegex)
            .WithMessage(ValidationConstants.PasswordRegexErrorMessage);
    }
}