using FluentValidation;

namespace Identity.Application.Features.UserRoles.Commands.Create;

public class UserRoleCreateCommandValidator : AbstractValidator<UserRoleCreateCommand>
{
    public UserRoleCreateCommandValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role Id cannot be empty")
            .NotNull().WithMessage("Role Id cannot be empty");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id cannot be empty")
            .NotNull().WithMessage("User Id cannot be empty");
    }
}