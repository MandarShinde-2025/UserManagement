using FluentValidation;
using UserManagement.Application.CQRS.Commands.Orders;

namespace UserManagement.Application.Validations;

public class UpdateOrderCommandValidator: AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.UserId!).GreaterThan(0).WithMessage("Please provide user id!");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order must contains at least one item!");
        RuleFor(x => x.Order.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than 0!");
        RuleForEach(x => x.Order.OrderItems).SetValidator(new OrderItemValidator());
    }
}
