using FluentValidation;
using UserManagement.Application.CQRS.Commands.MenuItems;

namespace UserManagement.Application.Validations;

public class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommand>
{
    public UpdateMenuItemCommandValidator()
    {
        RuleFor(x => x.MenuItem.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.MenuItem.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}