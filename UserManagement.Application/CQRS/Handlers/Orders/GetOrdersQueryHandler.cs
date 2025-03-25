using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Orders;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly RedisCacheService _cacheService;
    private readonly string key = "all_orders";

    public GetOrdersQueryHandler(IOrderRepository repository, IMapper mapper, RedisCacheService cacheService)
    {
        _repository = repository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var cachedOrders = await _cacheService.GetAsync<List<OrderDto>>(key);
        if (cachedOrders is not null) return cachedOrders;

        var orders = await _repository.GetAllAsync();
        var mappedOrders = orders.Select(order => _mapper.Map<OrderDto>(order));

        await _cacheService.SetAsync(key, mappedOrders, TimeSpan.FromMinutes(10));
        return mappedOrders;
    }
}