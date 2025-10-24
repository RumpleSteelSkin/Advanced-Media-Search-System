using FluentValidation;

namespace Identity.Application.Features.Roles.Commands.Update;

public class RoleUpdateCommandValidator : AbstractValidator<RoleUpdateCommand>
{
    public RoleUpdateCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Role Id cannot be empty")
            .NotNull().WithMessage("Role Id cannot be empty");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("Role name cannot be null")
            .NotEmpty().WithMessage("Role name is required")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters");
    }
}