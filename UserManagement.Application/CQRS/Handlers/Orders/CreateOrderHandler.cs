using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Commands.Orders;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.Order);

        await _repository.AddAsync(order);

        return order.Id;
    }
}