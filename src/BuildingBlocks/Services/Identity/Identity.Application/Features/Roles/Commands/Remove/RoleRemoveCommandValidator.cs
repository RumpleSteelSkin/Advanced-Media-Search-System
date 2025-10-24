using FluentValidation;

namespace Identity.Application.Features.Roles.Commands.Remove;

public class RoleRemoveCommandValidator : AbstractValidator<RoleRemoveCommand>
{
    public RoleRemoveCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Role Id cannot be empty")
            .NotNull().WithMessage("Role Id cannot be empty");
    }
}