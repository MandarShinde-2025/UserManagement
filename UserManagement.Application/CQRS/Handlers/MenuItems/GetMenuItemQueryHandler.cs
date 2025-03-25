using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.MenuItems;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class GetMenuItemQueryHandler : IRequestHandler<GetMenuItemQuery, MenuItemDto?>
{
    private readonly IMenuItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly RedisCacheService _cacheService;
    private readonly string key = "menu_item_";

    public GetMenuItemQueryHandler(IMenuItemRepository repository, IMapper mapper, RedisCacheService cacheService)
    {
        _repository = repository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<MenuItemDto?> Handle(GetMenuItemQuery request, CancellationToken cancellationToken)
    {
        var cachedMenuItem = await _cacheService.GetAsync<MenuItemDto>(key + request.Id);
        if (cachedMenuItem is not null) return cachedMenuItem;

        var menuItem = await _repository.GetByIdAsync(request.Id);
        if (menuItem is null) return null!;

        var mappedMenuItem = _mapper.Map<MenuItemDto>(menuItem);
        await _cacheService.SetAsync(key + request.Id, mappedMenuItem, TimeSpan.FromMinutes(10));

        return mappedMenuItem;
    }
}