using MediatR;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Order;
        var order = await _orderRepository.GetByIdAsync(dto.Id);
        if (order is null) return false;

        order.UserId = dto.UserId;

        // Remove items that are not in the request
        order.OrderItems!.RemoveAll(item => !dto.OrderItems.Any((i) => i.MenuItemId == item.MenuItemId));

        // Add or update items
        foreach (var item in dto.OrderItems)
        {
            var existingItem = order.OrderItems.FirstOrDefault((i) => i.MenuItemId == item.MenuItemId);
            if (existingItem is null)
            {
                order.OrderItems.Add(new Domain.Entities.OrderItem()
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }
            else
            {
                existingItem.Quantity = item.Quantity;
                existingItem.Price = item.Price;
            }
        }

        order.TotalAmount = request.Order.OrderItems.Sum((p) => p.Price);

        return await _orderRepository.UpdateAsync(order);
    }
}