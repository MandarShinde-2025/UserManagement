using FluentValidation;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Validations;

public class OrderItemValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.MenuItemId).GreaterThan(0).WithMessage("Please provide menu item id!");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0!");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0!");
    }
}