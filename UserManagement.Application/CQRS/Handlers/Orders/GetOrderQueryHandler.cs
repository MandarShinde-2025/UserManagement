using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.Orders;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Application.CQRS.Handlers.Orders;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto?>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly RedisCacheService _cacheService;
    private readonly string key = "order_";

    public GetOrderQueryHandler(IOrderRepository repository, IMapper mapper, RedisCacheService cacheService)
    {
        _repository = repository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<OrderDto?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var cachedOrder = await _cacheService.GetAsync<OrderDto>(key + request.Id);
        if (cachedOrder is not null) return cachedOrder;

        var order = await _repository.GetByIdAsync(request.Id);
        if (order is null) return null;

        var mappedOrder = _mapper.Map<OrderDto>(order);
        await _cacheService.SetAsync(key + request.Id, mappedOrder, TimeSpan.FromMinutes(10));

        return mappedOrder;
    }
}