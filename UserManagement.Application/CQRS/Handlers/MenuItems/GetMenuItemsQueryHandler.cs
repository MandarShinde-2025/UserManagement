using AutoMapper;
using MediatR;
using UserManagement.Application.CQRS.Queries.MenuItems;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Services;

namespace UserManagement.Application.CQRS.Handlers.MenuItems;

public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, IEnumerable<MenuItemDto>>
{
    private readonly IMenuItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly RedisCacheService _cacheService;
    private readonly string key = "all_menu_items";

    public GetMenuItemsQueryHandler(IMenuItemRepository repository, IMapper mapper, RedisCacheService cacheService)
    {
        _repository = repository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<MenuItemDto>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var cachedMenuItems = await _cacheService.GetAsync<List<MenuItemDto>>(key);
        if (cachedMenuItems is not null) return cachedMenuItems;

        var menuItems = await _repository.GetAllAsync();
        var mappedMenuItems = menuItems.Select(menuItem => _mapper.Map<MenuItemDto>(menuItem)).ToList();
        await _cacheService.SetAsync(key, mappedMenuItems, TimeSpan.FromMinutes(10));
        return mappedMenuItems;
    }
}