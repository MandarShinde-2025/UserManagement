using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Orders;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto?>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id);
        if (order is null) return null;

        return _mapper.Map<OrderDto>(order);
    }
}