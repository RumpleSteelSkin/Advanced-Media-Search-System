using FluentValidation;

namespace Identity.Application.Features.UserRoles.Commands.Remove;

public class UserRoleRemoveCommandValidator : AbstractValidator<UserRoleRemoveCommand>
{
    public UserRoleRemoveCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role Id cannot be empty")
            .NotNull().WithMessage("Role Id cannot be empty");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id cannot be empty")
            .NotNull().WithMessage("User Id cannot be empty");
    }
}