using FluentValidation;

namespace Identity.Application.Features.UserRoles.Commands.Update;

public class UserRoleUpdateCommandValidator : AbstractValidator<UserRoleUpdateCommand>
{
    public UserRoleUpdateCommandValidator()
    {
        RuleFor(x => x.ExistRoleId)
            .NotEmpty().WithMessage("Exist Role Id cannot be empty")
            .NotNull().WithMessage("Exist Role Id cannot be empty");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id cannot be empty")
            .NotNull().WithMessage("User Id cannot be empty");

        RuleFor(x => x.NewRoleId)
            .NotEmpty().WithMessage("New Role Id cannot be empty")
            .NotNull().WithMessage("New Role Id cannot be empty");
    }
}