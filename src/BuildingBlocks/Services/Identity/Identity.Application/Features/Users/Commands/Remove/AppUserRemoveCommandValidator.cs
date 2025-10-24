using FluentValidation;

namespace Identity.Application.Features.Users.Commands.Remove;

public class AppUserRemoveCommandValidator : AbstractValidator<AppUserRemoveCommand>
{
    public AppUserRemoveCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty")
            .NotNull().WithMessage("Id cannot be null");
    }
}