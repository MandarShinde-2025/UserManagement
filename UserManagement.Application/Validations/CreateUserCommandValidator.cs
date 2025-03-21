using FluentValidation;
using UserManagement.Application.CQRS.Commands.Users;

namespace UserManagement.Application.Validations;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.User.Email).EmailAddress().WithMessage("Invalid email address");
        RuleFor(x => x.User.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.User.Role).NotEmpty().WithMessage("Role is required");
    }
}
