using FluentValidation;

namespace Identity.Application.Features.Roles.Commands.Create;

public class RoleCreateCommandValidator : AbstractValidator<RoleCreateCommand>
{
    public RoleCreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Role name cannot be null")
            .NotEmpty().WithMessage("Role name is required")
            .MinimumLength(3).WithMessage("Role name must be at least 3 characters")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters");
    }
}