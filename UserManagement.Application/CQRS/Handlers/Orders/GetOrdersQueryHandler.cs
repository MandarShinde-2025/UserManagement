using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Orders;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync();
        return orders.Select(order => _mapper.Map<OrderDto>(order));
    }
}