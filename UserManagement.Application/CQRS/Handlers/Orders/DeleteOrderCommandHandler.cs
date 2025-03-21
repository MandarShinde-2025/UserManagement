using MediatR;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order is null) return false;

        return await _orderRepository.DeleteAsync(request.Id);
    }
}